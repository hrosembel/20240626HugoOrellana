using PruebaCrecerAPI.BLL.Interfaces;
using PruebaCrecerAPI.DAL.Interfaces;
using PruebaCrecerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.BLL.Implementations
{
    public class DepartamentoService : IDepartamentoService
    {
        public Task<bool> Create(NuevoDepartamento? entity)
        {
            throw new NotImplementedException();
        }

        public Task<object?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<object?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
