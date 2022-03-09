namespace PetaVerseApi.Core.Entities
{
    public class Breed : BaseEntity
    {
        public string  BreedName         { get; set; } = String.Empty;
        public string  BreedDescription  { get; set; } = String.Empty;
        public string  ImageUrl          { get; set; } = String.Empty;

        public double  MinimunSize       { get; set; }
        public double  MaximumSize       { get; set; }

        public double  MinimumWeight     { get; set; }
        public double  MaximumWeight     { get; set; }

        public int     MinimumLifeSpan   { get; set; }
        public int     MaximumLifeSpan   { get; set; }

        public CoatType Coat             { get; set; }
        public string   Color            { get; set; } = String.Empty;
    }

    public enum CoatType
    {
        Medium, Heavy, Light
    }
}
