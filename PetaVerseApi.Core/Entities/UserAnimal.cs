

namespace PetaVerseApi.Core.Entities
{
    public class UserAnimal : BaseEntity
    {
        public string? UserId { get; set; }
        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }
        public User? User { get; set; }
    }
}
