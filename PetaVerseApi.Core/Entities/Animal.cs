
namespace PetaVerseApi.Core.Entities
{
    public class Animal : BaseEntity
    {
        public string    Name    { get; set; } = string.Empty;
        public bool      Gender  { get; set; }
        public int       Age     { get; set; }
        public Species?  Species { get; set; }
        public Breed?    Breed   { get; set; }
        public virtual ICollection<UserAnimal> UserAnimals { get; set; } = new HashSet<UserAnimal>();
    }
}
