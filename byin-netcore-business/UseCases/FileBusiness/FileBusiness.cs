using byin_netcore_business.Entity.File;
using byin_netcore_business.Interfaces;
using byin_netcore_business.UseCases.Base;
using System;
using System.IO;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.FileBusiness
{
    public class FileBusiness : BaseBusiness, IFileBusiness
    {
        private readonly IFilePathRepository _fileEntityRepository;
        private readonly ICloudStorage _cloudStorage;
        public FileBusiness(IFilePathRepository fileEntityRepository, ICloudStorage cloudStorage, IAuthorizationBusiness authorizationService) : base(authorizationService)
        {
            _fileEntityRepository = fileEntityRepository;
            _cloudStorage = cloudStorage;
        }

        public async Task<FilePath> AddFileAsync(Stream fileContent)
        {
            var file = await _cloudStorage.UploadFileAsync(fileContent).ConfigureAwait(false);
            try
            {
                var filePath = new FilePath 
                {
                    CloudStorageKey = file.StorageKey,
                    ImagePath = file.MediaLink
                };
                return await _fileEntityRepository.InsertAsync(filePath).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                await _cloudStorage.DeleteFileAsync(file.StorageKey).ConfigureAwait(false);
                throw e;
            }
        }

        public async Task DeleteFileAsync(string storageKey)
        {
            var fileEntity = await _fileEntityRepository.GetFileByKeyInCloudStorageAsync(storageKey).ConfigureAwait(false);
            if(fileEntity is null)
            {
                return;
            }
            await _cloudStorage.DeleteFileAsync(fileEntity.CloudStorageKey).ConfigureAwait(false);
        }

        public async Task DeleteFileAsync(int id)
        {
            var fileEntity = await _fileEntityRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (fileEntity is null)
            {
                return;
            }
            await _cloudStorage.DeleteFileAsync(fileEntity.CloudStorageKey).ConfigureAwait(false);
        }
    }
}
