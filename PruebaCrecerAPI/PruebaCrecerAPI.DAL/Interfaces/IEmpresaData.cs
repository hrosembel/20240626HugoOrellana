using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.DAL.Interfaces
{
    public interface IEmpresaData
    {
        Task<Models.Empresa> ObtenerEmpresaPorNIT(string NIT);
    }
}
