

namespace PetaVerseApi.Core.Entities
{
    public class UserAnimal : BaseEntity
    {
        public string   UserId      { get; set; } = null!;
        public string   AnimalId    { get; set; } = null!;

        public Animal?  Animal  { get; set; }
        public User?    User    { get; set; }
    }
}
