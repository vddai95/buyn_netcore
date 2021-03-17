using AutoMapper;
using byin_netcore_business.Entity.File;
using byin_netcore_business.Interfaces;
using byin_netcore_data.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using BL = byin_netcore_business.Entity;
using DL = byin_netcore_data.Model;

namespace byin_netcore_data.BusinessImplementation
{
    public class FilePathRepository : Repository<BL.File.FilePath, DL.FilePath>, IFilePathRepository
    {
        public FilePathRepository(IMapper mapper, IEntityRepository<DL.FilePath> entityRepository) : base(mapper, entityRepository)
        {

        }

        public async Task<FilePath> GetFileByKeyInCloudStorageAsync(string keyStorage)
        {
            var result = await _entityRepository.GetWhereAsync(fp => fp.CloudStorageKey == keyStorage).ConfigureAwait(false);
            if(result is null || !result.Any())
            {
                return null;
            }

            return _mapper.Map<FilePath>(result.FirstOrDefault());
        }
    }
}
