using Microsoft.AspNetCore.Identity;

namespace PetaVerseApi.Core.Entities
{
    public class User : IdentityUser
    {
        public string Guid { get; set; } = null!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Status> Statuses { get; set; } = new HashSet<Status>();
        public virtual ICollection<UserRole> UserRoles { get; } = new HashSet<UserRole>();
        public virtual ICollection<UserAnimal> UserAnimals { get; set; } = new HashSet<UserAnimal>();
    }
}
