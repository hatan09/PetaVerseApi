namespace PetaVerseApi.Core.Entities
{
    public class User : BaseEntity
    {
        public string       Guid                     { get; set; } = string.Empty;
        public string?      PetaverseProfileImageUrl { get; set; }
        public string?      CoverImageUrl            { get; set; }
        public DateTime     CreatedAt                { get; set; } = DateTime.UtcNow;
        public bool?        IsActive                 { get; set; }
        public bool         IsDeleted                { get; set; } = false;

        public virtual ICollection<Status>      Statuses        { get; set; } = new HashSet<Status>();
        public virtual ICollection<UserRole>    UserRoles       { get; }      = new HashSet<UserRole>();
        public virtual ICollection<UserAnimal>  UserAnimals     { get; set; } = new HashSet<UserAnimal>();
    }
}
