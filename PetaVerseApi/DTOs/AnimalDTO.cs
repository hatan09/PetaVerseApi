using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.DTOs
{
    public class AnimalDTO : BaseDTO
    {
        public string   Name         { get; set; } = string.Empty;
        public string   Bio          { get; set; } = string.Empty;
        public string?  PetColor     { get; set; } = string.Empty;
        public string?  SixDigitCode { get; set; } = string.Empty;
        public bool     Gender       { get; set; }
        public double   Age          { get; set; }
        public int      SpeciesId    { get; set; }

        public int       BreedId      { get; set; }
        public BreedDTO? Breed        { get; set; }

        public int?               PetAvatarId { get; set; }
        public PetaverseMediaDTO? PetAvatar   { get; set; }

        //public virtual ICollection<int>               UserAnimals     { get; set; } = Array.Empty<int>();
        public virtual ICollection<PetaverseMediaDTO>?   PetPhotos       { get; set; } = Array.Empty<PetaverseMediaDTO>();
    }

    public class AnimalDetailDTO : AnimalDTO
    {
    }

    public class PetDTO : AnimalDTO
    {
        public string OwnerGuids { get; set; } = string.Empty;
    }

    public class FeralAnimalDTO : AnimalDTO
    {

    }

    public class UploadAnimalImageDTO
    {
        public int                    PetId     { get; set; }
        public ICollection<IFormFile> PetPhotos { get; set; } = Array.Empty<IFormFile>();
    }
}
