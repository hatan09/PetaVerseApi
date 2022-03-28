namespace PetaVerseApi.Core.Entities
{
    public class Breed : BaseEntity
    {
        public int          SpeciesId           { get; set; }
        public string       BreedName           { get; set; } = string.Empty;
        public string       BreedDescription    { get; set; } = string.Empty;
        public string       ImageUrl            { get; set; } = string.Empty;
        public double       MinimunSize         { get; set; }
        public double       MaximumSize         { get; set; }
        public double       MinimumWeight       { get; set; }
        public double       MaximumWeight       { get; set; }
        public int          MinimumLifeSpan     { get; set; }
        public int          MaximumLifeSpan     { get; set; }
        public CoatType     Coat                { get; set; }
        public string       Color               { get; set; } = string.Empty;

        public Species?     Species     { get; set; }

        public virtual ICollection<Animal>  Animals     { get; set; } = new HashSet<Animal>();
    }

    public enum CoatType
    {
        Medium, 
        Heavy, 
        Light
    }
}
