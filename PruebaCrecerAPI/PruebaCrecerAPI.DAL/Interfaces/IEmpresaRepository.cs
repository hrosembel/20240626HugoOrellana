using PruebaCrecerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.DAL.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<NuevaEmpresa?> ObtenerEmpresaPorNIT(string NIT);
    }
}
