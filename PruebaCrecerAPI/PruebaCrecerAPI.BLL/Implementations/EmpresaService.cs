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
    public class EmpresaService : IEmpresaService
    {
        private readonly IGenericRepository<NuevaEmpresa> _repository;
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaService(IGenericRepository<NuevaEmpresa> repository, IEmpresaRepository empresaRepository)
        {
            _repository = repository;
            _empresaRepository = empresaRepository;
        }
        public async Task<ICollection<NuevaEmpresa>> GetAll() 
            => await _repository.GetAll();
        
        public async Task<NuevaEmpresa?> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID no válido");
            }

            return await _repository.GetById(id);
        }

        public async Task<int> Create(NuevaEmpresa entity)
            => await _repository.Create(entity);

        public async Task<bool> Update(NuevaEmpresa entity)
            => await _repository.Update(entity);

        public async Task<bool> Delete(int id) 
            => await _repository.Delete(id);

        public async Task<NuevaEmpresa> GetByNIT(string NIT)
            => await _empresaRepository.ObtenerEmpresaPorNIT(NIT);
    }
}
