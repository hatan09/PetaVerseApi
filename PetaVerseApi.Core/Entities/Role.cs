using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PetaVerseApi.Core.Entities
{
    public class Role : BaseEntity
    {
        public string   Name    { get; set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; } = new HashSet<UserRole>();
    }
}