namespace PetaVerseApi.Interfaces
{
    public interface IMediaService
    {
        public bool IsImage(IFormFile file);
        public Task<Tuple<string, string, string>> UploadFileToStorage(Stream fileStream, string fileName, int petId);
        public Task<Tuple<string, string>> UploadAvatarToStorage(Stream fileStream, string fileName);
        public Task<List<string>> GetThumbNailUrls();
    }
}
