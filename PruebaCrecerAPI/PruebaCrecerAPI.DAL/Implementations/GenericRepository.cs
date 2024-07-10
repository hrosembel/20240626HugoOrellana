using Dapper;
using Microsoft.Extensions.Options;
using PruebaCrecerAPI.DAL.Interfaces;
using PruebaCrecerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static Dapper.SqlMapper;

namespace PruebaCrecerAPI.DAL.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
        protected readonly ConnectionStrings _connectionStrings;
        private string TableName { get; }
        public GenericRepository(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
            TableName = GetTableName();
        }
        public async Task<ICollection<T>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                db.Open();
                return (await db.QueryAsync<T>($"SELECT * FROM {TableName}")).ToList();
            }
        }

        public async Task<T?> GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                db.Open();
                return await db.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {TableName} WHERE Id = @id");
            }
        }

        public async Task<int> Create(T entity)
        {
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                string columns = GetColumns(excludeKey: true);
                string properties = GetPropertyNames(excludeKey: true);
                string query = $"INSERT INTO {TableName} ({columns}) " +
                    $"OUTPUT INSERTED.Id" +
                    $" VALUES ({properties})";

                db.Open();
                int id = await db.QueryFirstOrDefaultAsync(query, entity);
                return id;
            }
        }

        public async Task<bool> Update(T entity)
        {
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                int rowsEffected = 0;

                StringBuilder query = new StringBuilder();
                query.Append($"UPDATE {TableName} SET ");

                foreach (var property in GetProperties(true))
                {
                    var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

                    string propertyName = property.Name;
                    string columnName = columnAttr.Name;

                    query.Append($"{columnName} = @{propertyName},");
                }

                query.Remove(query.Length - 1, 1);

                query.Append($" WHERE Id = @id");

                db.Open();
                rowsEffected = await db.ExecuteAsync(query.ToString(), entity);

                return rowsEffected > 0;
            }           
        }
        public async Task<bool> Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                db.Open();
                int rowsEffected = 0;
                string query = $"DELETE {TableName} WHERE Id = @id";

                rowsEffected = await db.ExecuteAsync(query, new { id = id});
                return rowsEffected > 0;
            }

        }

        private string GetColumns(bool excludeKey = false)
        {
            var type = typeof(T);
            var columns = string.Join(", ", type.GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
                .Select(p =>
                {
                    var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttr != null ? columnAttr.Name : p.Name;
                }));

            return columns;
        }

        protected string GetPropertyNames(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

            var values = string.Join(", ", properties.Select(p =>
            {
                return $"@{p.Name}";
            }));

            return values;
        }
        protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

            return properties;
        }

        private string GetTableName()
        {
            string tableName = "";
            var type = typeof(T);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            if (tableAttr != null)
            {
                tableName = tableAttr.Name;
                return tableName;
            }

            return type.Name;
        }
    }
}
