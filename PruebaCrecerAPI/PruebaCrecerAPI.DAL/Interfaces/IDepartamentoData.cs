using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.DAL.Interfaces
{
    public interface IDepartamentoData
    {
        Task<List<Models.Departamento>> ObtenerPorNITEmpresa(string NIT);
    }
}
