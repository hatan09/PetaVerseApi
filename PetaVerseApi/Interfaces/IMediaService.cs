using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Interfaces
{
    public interface IMediaService
    {
        bool IsImage(IFormFile file);
        Task<Tuple<string, string, string>> UploadFileToStorage(Stream fileStream, string fileName, int petId);
        Task<Tuple<string, string>> UploadAvatarToStorage(Stream fileStream, string fileName);
        Task<List<string>> GetThumbNailUrls();
        //Task DeleteFilesAsync(List<string> fileListName);
        Task DeleteFileAsync(string fileName, MediaType mediaType);
    }
}
