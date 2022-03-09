
namespace PetaVerseApi.Core.Entities
{
    public class Animal : BaseEntity
    {
        public string    Name    { get; set; } = String.Empty;
        public bool      Gender  { get; set; }
        public DateOnly? Age     { get; set; }
        public Species?  Species { get; set; }
        public Breed?    Breed   { get; set; }

        public virtual ICollection<UserAnimal> UserAnimals { get; set; } = new HashSet<UserAnimal>();
    }
}
