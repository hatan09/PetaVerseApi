using Microsoft.AspNetCore.Identity;

namespace PetaVerseApi.Core.Entities
{
    public class UserRole :BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string RoleId { get; set; } = null!;

        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
    }
}
