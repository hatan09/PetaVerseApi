namespace PetaVerseApi.Core.Entities
{
    public class Species : BaseEntity
    {
        public string             Name        { get; set; } = string.Empty;
        public string             Description { get; set; } = string.Empty;

        public ICollection<Breed>   Breeds      { get; set; } = new HashSet<Breed>(); 
    }
}
