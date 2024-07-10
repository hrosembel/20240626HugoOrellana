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

namespace PruebaCrecerAPI.DAL.Implementations
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        public EmpresaRepository(IOptions<ConnectionStrings> options)
            => _connectionStrings = options.Value;
        public async Task<NuevaEmpresa?> ObtenerEmpresaPorNIT(string NIT)
        {
            NuevaEmpresa? empresa = new();
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                db.Open();
                // Llamada al stored procedure usando Dapper
                var parameters = new { NIT = NIT };
                empresa = await db.QueryFirstOrDefaultAsync<NuevaEmpresa>("ObtenerEmpresaPorNIT", parameters, commandType: CommandType.StoredProcedure);

            }
            return empresa;
        }
    }
}
