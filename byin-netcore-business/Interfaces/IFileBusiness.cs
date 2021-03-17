using byin_netcore_business.Entity;
using byin_netcore_business.Entity.File;
using byin_netcore_business.Inputs;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IFileBusiness
    {
        Task<FilePath> AddFileAsync(Stream fileContent);
        Task DeleteFileAsync(string KeyOnCloud);
        Task DeleteFileAsync(int id);
    }
}
