﻿using Dapper;
using PruebaCrecerAPI.DAL.Interfaces;
using PruebaCrecerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PruebaCrecerAPI.DAL.Implementations
{
    public class DepartamentoData : IDepartamentoData
    {
        private readonly ConnectionStrings _connectionStrings;
        public DepartamentoData(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
        }

        public async Task<bool> AgregarDepartamento(NuevoDepartamento nuevoDepartamento)
        {
            var successful = false;
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                // Consulta SQL para la inserción
                string sqlQuery = "INSERT INTO EmpresaDepartamento (IdEmpresa, IdDepartamento, NumeroEmpleados) VALUES (@IdEmpresa, @IdDepartamento, @NumeroEmpleados);";

                // Ejecutamos la consulta utilizando Dapper
                successful = db.Execute(sqlQuery, nuevoDepartamento) > 0;
            }
            return successful;
        }

        public async Task<List<Departamento>> ObtenerPorNITEmpresa(string NIT)
        {
            List<Models.Departamento> departamentos = new();
            using (IDbConnection db = new SqlConnection(_connectionStrings.SqlConnectionString))
            {
                db.Open();
                // Llamada al stored procedure usando Dapper
                var parameters = new { NIT = NIT };
                var result = db.Query<Departamento>("ObtenerDepartamentosPorNIT", parameters, commandType: CommandType.StoredProcedure);
                departamentos = result.ToList();
            }
            return departamentos;
        }
    }
}
