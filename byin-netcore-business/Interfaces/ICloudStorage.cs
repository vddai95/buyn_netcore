using byin_netcore_business.Entity.File;
using System.IO;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface ICloudStorage
    {
        Task<FileOnCloud> UploadFileAsync(Stream content);
        Task DeleteFileAsync(string objectName);
        Task<FileOnCloud> GetFileAsync(string objectName);
        Task DownloadFileAsync(string objectName, Stream destination);
    }
}
