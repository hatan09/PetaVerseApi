
namespace PetaVerseApi.Core.Entities
{
    public class Animal : BaseEntity
    {
        public string       Name         { get; set; } = string.Empty;
        public string?      Bio          { get; set; } = string.Empty;
        public string?      PetColor     { get; set; } = string.Empty;
        public string?      SixDigitCode { get; set; } = string.Empty;
        public bool         Gender       { get; set; }
        public double?      Age          { get; set; }
        public DateTime?    DateOfBirth  { get; set; }

        public int?            BreedId      { get; set; }
        public Breed?          Breed        { get; set; }

        public int?            PetAvatarId  { get; set; }
        public PetaverseMedia? PetAvatar    { get; set; }

        public virtual ICollection<UserAnimal>           UserAnimals           { get; set; } = new HashSet<UserAnimal>();
        public virtual ICollection<AnimalPetaverseMedia> AnimalPetaverseMedias { get; set; } = new HashSet<AnimalPetaverseMedia>();
    }
}
