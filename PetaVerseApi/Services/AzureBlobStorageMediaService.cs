using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using PetaVerseApi.AppSettings;
using PetaVerseApi.Interfaces;

namespace PetaVerseApi.Services
{
    public class AzureBlobStorageMediaService : IMediaService
    {
        private readonly IOptionsMonitor<AzureStorageConfig> _storageConfig;
        private StorageSharedKeyCredential                   _storageCredentials;
        public  AzureBlobStorageMediaService(IOptionsMonitor<AzureStorageConfig> azureStorageConfig,
                                             StorageSharedKeyCredential storageCredentials)
        {
            _storageConfig = azureStorageConfig;
            _storageCredentials = storageCredentials;
        }

        public Task<List<string>> GetThumbNailUrls()
        {
            throw new NotImplementedException();
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
                                  ".blob.core.windows.net/" +
                                  _storageConfig.CurrentValue.ImageContainer +
                                  "/" + fileName);
            // Create the blob client.
            var blobClient = new BlobClient(blobUri, _storageCredentials);
            await blobClient.SetTagsAsync(new Dictionary<string, string>
            {
                { "Guid", blobGuid },
                { "PetId", petId.ToString() },
                { "UploadDate", DateTime.Now.ToShortDateString() }
            });

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

        public async Task DeleteFilesAsync(List<string> fileGuids)
        {
            //build up Query
            //get tagged blob items from the BlobServiceClient 
        }

        public async Task<ICollection<string>> GetAllFilesAsync()
        {
            var listOfBlobNames = new List<string>();
            var blobContainerClient = new BlobContainerClient(_storageConfig.CurrentValue.BlobContainerUrl,
                                                              _storageConfig.CurrentValue.ImageContainer);
            try
            {
                // Call the listing operation and return pages of the specified size.
                var allBlobs = blobContainerClient.GetBlobsAsync()
                                                  .AsPages(default);

                await foreach (var blob in allBlobs)
                {
                    foreach (BlobItem blobItem in blob.Values)
                    {
                        listOfBlobNames.Add(blobItem.Name);
                    }
                };

            }
            catch (RequestFailedException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
            return listOfBlobNames;
        }
    }
}
