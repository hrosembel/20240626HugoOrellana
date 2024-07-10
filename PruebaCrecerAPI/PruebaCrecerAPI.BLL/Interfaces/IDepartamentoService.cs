using PruebaCrecerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.BLL.Interfaces
{
    public interface IDepartamentoService
    {
        Task<object?> GetAll();
        Task<object?> GetById(int id);

        Task<bool> Create(NuevoDepartamento? entity);
    }
}
