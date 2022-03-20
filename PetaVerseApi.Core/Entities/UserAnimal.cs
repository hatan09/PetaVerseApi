

namespace PetaVerseApi.Core.Entities
{
    public class UserAnimal : BaseEntity
    {
        public Animal? Animal { get; set; }
        public User? User { get; set; }
    }
}
