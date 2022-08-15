namespace PetaVerseApi.Core.Entities
{
    public class PetaverseMedia : BaseEntity
    {
        public string       MediaName   { get; set; } = string.Empty;
        public string       MediaGuid   { get; set; } = string.Empty;
        public string       MediaUrl    { get; set; } = string.Empty;
        public DateTime     TimeUpload  { get; set; }
        public MediaType    Type        { get; set; }


        public virtual ICollection<AnimalPetaverseMedia> AnimalPetaverseMedias { get; set; } = new HashSet<AnimalPetaverseMedia>();
    }
    public enum MediaType
    {
        Video, Photo
    }
}
