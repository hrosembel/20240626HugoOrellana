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
        public async Task<Models.Empresa> ObtenerEmpresaPorNIT(string NIT)
        {
            Models.Empresa empresa = new ();
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                db.Open();
                // Llamada al stored procedure usando Dapper
                var parameters = new { NIT = NIT };
                var empresas = db.Query<Empresa>("ObtenerEmpresaPorNIT", parameters, commandType: CommandType.StoredProcedure);

                empresa = empresas.FirstOrDefault();
            }
            return empresa;
        }
    }
}
