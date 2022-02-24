using Microsoft.AspNetCore.Identity;

namespace PetaVerseApi.Core.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
    }
}
