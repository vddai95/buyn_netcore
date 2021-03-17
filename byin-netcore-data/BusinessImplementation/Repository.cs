using AutoMapper;
using byin_netcore_business.Interfaces;
using byin_netcore_data.Interfaces;
using System.Threading.Tasks;

namespace byin_netcore_data.BusinessImplementation
{
    public class Repository<B, D> : IRepository<B> 
        where B : class 
        where D : class
    {
        protected readonly IMapper _mapper;
        protected readonly IEntityRepository<D> _entityRepository;
        public Repository(IMapper mapper, IEntityRepository<D> entityRepository)
        {
            _mapper = mapper;
            _entityRepository = entityRepository;
        }

        public async Task DeleteAsync(B entity)
        {
            D dlEntity = _mapper.Map<D>(entity);
            await _entityRepository.DeleteAsync(dlEntity).ConfigureAwait(false);
        }

        public async Task<B> GetByIdAsync(object id)
        {
            D dlEntity = await _entityRepository.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<B>(dlEntity);
        }

        public async Task<B> InsertAsync(B entity)
        {
            D dlEntity = _mapper.Map<D>(entity);
            await _entityRepository.InsertAsync(dlEntity).ConfigureAwait(false);
            return _mapper.Map<B>(dlEntity);
        }

        public async Task<B> UpdateAsync(B entity)
        {
            D dlEntity = _mapper.Map<D>(entity);
            await _entityRepository.UpdateAsync(dlEntity).ConfigureAwait(false);
            return _mapper.Map<B>(dlEntity);
        }
    }
}
