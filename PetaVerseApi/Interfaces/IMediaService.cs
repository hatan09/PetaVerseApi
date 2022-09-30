using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Interfaces
{
    public interface IMediaService
    {
        //Task DeleteFilesAsync(List<string> fileListName);
        Task DeleteFileAsync(string fileName, MediaType mediaType);
        Task<PetaverseMedia> UploadFileToStorage(Stream fileStream,
                                                 string fileName,
                                                 MediaType type,
                                                 string container);
    }
}
