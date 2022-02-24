
namespace PetaVerseApi.Core.Entities
{
    public class Animal : BaseEntity
    {
        public int Name { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }

        public virtual ICollection<UserAnimal> UserAnimals { get; set; } = new HashSet<UserAnimal>();
    }
}
