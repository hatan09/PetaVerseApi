namespace PetaVerseApi.DTOs
{
    public class PetaverseMediaDTO : BaseDTO
    {
        public string       MediaUrl    { get; set; } = string.Empty;
        public DateTime     TimeUpload  { get; set; }
        public MediaType    Type        { get; set; }
    }
    public enum MediaType
    {
        Video, Photo
    }
}
