using Azure.Storage.Blobs;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using PetaVerseApi.Enums;
using PetaVerseApi.Helpers;
using PetaVerseApi.Interfaces;
using System.Threading;
using MediaType = PetaVerseApi.Core.Entities.MediaType;

namespace PetaVerseApi.Services
{
    public class MediaService : IMediaService
    {
        private AzureStorageAccountHelper _accountHelper;
        private BlobContainerClient _avatarBlobContainerClient;
        private BlobContainerClient _galleryBlobContainerClient;
        public MediaService(AzureStorageAccountHelper accountHelper,
                            Func<ContainerEnum, BlobContainerClient> avatarBlobContainerClient,
                            Func<ContainerEnum, BlobContainerClient> galleryBlobContainerClient)
        {
            _accountHelper = accountHelper;
            _avatarBlobContainerClient = avatarBlobContainerClient(ContainerEnum.AvatarContainer);
            _galleryBlobContainerClient = galleryBlobContainerClient(ContainerEnum.GalleryContainer);
        }

        public async Task<PetaverseMedia> UploadFileToStorage(Stream fileStream, 
                                                              string fileName, 
                                                              MediaType type,
                                                              string containerName)
            => await _accountHelper.UploadFileToStorage(fileStream, fileName, type, containerName);

        public async Task DeleteFileAsync(string fileName, MediaType mediaType)
        {
            var blob = mediaType == MediaType.Photo
                     ? _galleryBlobContainerClient.GetBlobClient(fileName)
                     : _avatarBlobContainerClient.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }
    }
}
