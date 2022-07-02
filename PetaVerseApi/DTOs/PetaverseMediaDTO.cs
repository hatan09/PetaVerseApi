namespace PetaVerseApi.DTOs
{
    public class PetaverseMediaDTO : BaseDTO
    {
        public string       MediaUrl    { get; set; } = string.Empty;
        public DateTime     TimeUpload  { get; set; }
        public string       Type        { get; set; } = string.Empty ;
    }
}
