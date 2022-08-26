using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PetaVerseApi.Interfaces;
using PetaVerseApi.AppSettings;
using Microsoft.Extensions.Options;
using PetaVerseApi.Enums;
using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Services
{
    public class AzureBlobStorageMediaService : IMediaService
    {
        private readonly IOptionsMonitor<AzureStorageConfig> _storageConfig;
        private StorageSharedKeyCredential                   _storageCredentials;
        private BlobContainerClient                          _avatarBlobContainerClient;
        private BlobContainerClient                          _galleryBlobContainerClient;
        public AzureBlobStorageMediaService(IOptionsMonitor<AzureStorageConfig> azureStorageConfig,
                                            StorageSharedKeyCredential storageCredentials,
                                            Func<ContainerEnum, BlobContainerClient> avatarBlobContainerClient,
                                            Func<ContainerEnum, BlobContainerClient> galleryBlobContainerClient)
        {
            _storageConfig              = azureStorageConfig;
            _storageCredentials         = storageCredentials;
            _avatarBlobContainerClient  = avatarBlobContainerClient(ContainerEnum.AvatarContainer);
            _galleryBlobContainerClient = galleryBlobContainerClient(ContainerEnum.GalleryContainer);
        }

        public bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Tuple<string, string, string>> UploadFileToStorage(Stream fileStream,
                                                                             string fileName,
                                                                             int petId)
        {
            var blobGuid = Guid.NewGuid().ToString("N");
            var blobUri = new Uri("https://" +
                                  _storageConfig.CurrentValue.AccountName +
                                  ".blob.core.windows.net/petaversegallery" +
                                  "/" + fileName);
            // Create the blob client.
            var blobClient = new BlobClient(blobUri, _storageCredentials);
            // Upload the file
            await blobClient.UploadAsync(fileStream);

            return new Tuple<string, string, string>(blobClient.Name, blobClient.Uri.AbsoluteUri, blobGuid);
        }

        public async Task<Tuple<string, string>> UploadAvatarToStorage(Stream fileStream,
                                                                     string fileName)
        {
            var blobUri = new Uri("https://" +
                                  _storageConfig.CurrentValue.AccountName +
                                  ".blob.core.windows.net/petaverseavatar" +
                                  "/" + fileName);
            // Create the blob client.
            var blobClient = new BlobClient(blobUri, _storageCredentials);

            // Upload the file
            await blobClient.UploadAsync(fileStream);

            return new Tuple<string, string>(blobClient.Name, blobClient.Uri.AbsoluteUri);
        }

        public async Task DeleteFilesAsync(List<string> fileListName, MediaType mediaType)
        {
            foreach (var fileName in fileListName)
            {
                try
                {
                    var blob = mediaType == MediaType.Photo
                                         ? _galleryBlobContainerClient.GetBlobClient(fileName)
                                         : _avatarBlobContainerClient.GetBlobClient(fileName);
                    await blob.DeleteIfExistsAsync();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task DeleteFileAsync(string fileName, MediaType mediaType)
        {
            try
            {
                var blob = mediaType == MediaType.Photo 
                                     ? _galleryBlobContainerClient.GetBlobClient(fileName)
                                     : _avatarBlobContainerClient.GetBlobClient(fileName);
                await blob.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<ICollection<string>> GetAllFilesAsync()
        {
            return null;
        }
    }
}
