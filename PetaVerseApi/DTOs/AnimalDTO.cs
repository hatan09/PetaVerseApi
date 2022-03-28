namespace PetaVerseApi.DTOs
{
    public class AnimalDTO : BaseDTO
    {
        public string   Name        { get; set; } = string.Empty;
        public bool     Gender      { get; set; }
        public int      Age         { get; set; }
        public int      SpeciesId   { get; set; }
        public int      BreedId     { get; set; }

        public virtual ICollection<int>     UserAnimals     { get; set; } = Array.Empty<int>();
    }
}
