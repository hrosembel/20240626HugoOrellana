using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.DAL.Interfaces
{
    //IdT se refiere al tipo que se estara usando para el id de la entidad
    public interface IGenericRepository<T> where T : class
    {
        Task<int> Create(T entity);
        Task<T?> GetById(int id);
        Task<ICollection<T>> GetAll();//Se usa IQueriable para que la consulta se haga directamente en sql y no en memoria
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }
}
