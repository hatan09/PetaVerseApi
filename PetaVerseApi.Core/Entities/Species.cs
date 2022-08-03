namespace PetaVerseApi.Core.Entities
{
    public class Species : BaseEntity
    {
        public string Name                 { get; set; } = string.Empty;
        public string Color                { get; set; } = string.Empty;
        public string Icon                 { get; set; } = string.Empty;
        public string Description          { get; set; } = string.Empty;
        public string TopLovedPetOfTheWeek { get; set; } = string.Empty;

        public virtual ICollection<Breed>       Breeds      { get; set; } = new HashSet<Breed>(); 
    }
}
