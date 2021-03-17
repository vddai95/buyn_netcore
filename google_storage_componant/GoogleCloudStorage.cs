using byin_netcore_business.Entity.File;
using byin_netcore_business.Interfaces;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Requests;
using Google.Cloud.Storage.V1;
using google_storage_componant.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace google_storage_componant
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential _googleCredential;
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public GoogleCloudStorage(IOptions<GoogleStorageConfiguration> googleStorageConfig)
        {
            var googleCloudStorageConfiguration = googleStorageConfig.Value;
            _googleCredential = GoogleCredential.FromFile(googleCloudStorageConfiguration.ConfigurationFile);
            _storageClient = StorageClient.Create(_googleCredential);
            _bucketName = googleCloudStorageConfiguration.BucketName;
        }

        public async Task DeleteFileAsync(string objectName)
        {
            await _storageClient.DeleteObjectAsync(_bucketName, objectName);
        }

        public async Task DownloadFileAsync(string objectName, Stream destination)
        {
            await _storageClient.DownloadObjectAsync(_bucketName, objectName, destination);
        }

        public async Task<FileOnCloud> GetFileAsync(string objectName)
        {
            var file = await _storageClient.GetObjectAsync(_bucketName, objectName).ConfigureAwait(false);
            if(file is null)
            {
                return null;
            }
            var result = new FileOnCloud
            {
                StorageKey = objectName,
                MediaLink = file.MediaLink
            };

            return result;
        }

        public async Task<FileOnCloud> UploadFileAsync(Stream content)
        {
            string objectName = string.Empty;
            var isUnique = false;
            do
            {
                try
                {
                    objectName = System.Guid.NewGuid().ToString();
                    var obj = await _storageClient.GetObjectAsync(_bucketName, objectName, new GetObjectOptions { Projection = Projection.NoAcl }).ConfigureAwait(false);
                }
                catch(GoogleApiException ex)
                {
                    if(ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        isUnique = true;
                    }
                    continue;
                }
            } while (!isUnique);
            
            using (var memoryStream = new MemoryStream())
            {
                await content.CopyToAsync(memoryStream).ConfigureAwait(false);
                var dataObject = await _storageClient.UploadObjectAsync(_bucketName, objectName, null, memoryStream);
                return new FileOnCloud
                { 
                    StorageKey = dataObject.Name,
                    MediaLink = dataObject.MediaLink
                };
            }
        }
    }
}
