using byin_netcore_business.Entity.File;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IFilePathRepository : IRepository<FilePath>
    {
        Task<FilePath> GetFileByKeyInCloudStorageAsync(string keyStorage);
    }
}
