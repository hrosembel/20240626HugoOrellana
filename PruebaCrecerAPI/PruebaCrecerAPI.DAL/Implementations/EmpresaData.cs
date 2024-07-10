using Dapper;
using Microsoft.Extensions.Options;
using PruebaCrecerAPI.DAL.Interfaces;
using PruebaCrecerAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace PruebaCrecerAPI.DAL.Implementations
{
    public class EmpresaData : IEmpresaData
    {
        private readonly ConnectionStrings _connectionStrings;
        public EmpresaData(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
        }

        public async Task<bool> AgregarEmpresa(NuevaEmpresa nuevaEmpresa)
        {
            var successful = false;
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                // Consulta SQL para la inserción
                string sqlQuery = "INSERT INTO Empresa (NIT, Nombre, RazonSocial, FechaRegistro, Bitacora) VALUES (@NIT, @Nombre, @RazonSocial, GETDATE(), @Bitacora);";

                // Ejecutamos la consulta utilizando Dapper
                successful = db.Execute(sqlQuery, nuevaEmpresa) > 0;
            }
            return successful;
        }

        public async Task<Models.NuevaEmpresa> ObtenerEmpresaPorNIT(string NIT)
        {
            Models.NuevaEmpresa empresa = new ();
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                db.Open();
                // Llamada al stored procedure usando Dapper
                var parameters = new { NIT = NIT };
                empresa = db.QueryFirst<NuevaEmpresa>("ObtenerEmpresaPorNIT", parameters, commandType: CommandType.StoredProcedure);

            }
            return empresa;
        }
    }
}
