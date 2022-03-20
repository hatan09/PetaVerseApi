namespace PetaVerseApi.Core.Entities
{
    public class Species : BaseEntity
    {
        public string             Name        { get; set; } = String.Empty;
        public string             Description { get; set; } = String.Empty;

        public ICollection<Breed> Breeds      { get; set; } = new HashSet<Breed>(); 
    }
}
