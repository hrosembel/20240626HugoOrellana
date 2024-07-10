using PruebaCrecerAPI.DAL.Interfaces;
using PruebaCrecerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.BLL.Interfaces
{
    public interface IEmpresaService
    {
        Task<ICollection<NuevaEmpresa>> GetAll();
        Task<NuevaEmpresa?> GetById(int id);
        Task<NuevaEmpresa> GetByNIT(string NIT);

        Task<int> Create(NuevaEmpresa entity);

        Task<bool> Update(NuevaEmpresa entity);

        Task<bool> Delete(int id);
    }
}
