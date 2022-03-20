using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PetaVerseApi.Core.Entities
{
    public class Role : BaseEntity
    {
        public virtual ICollection<UserRole> UserRoles { get; } = new HashSet<UserRole>();
    }
}