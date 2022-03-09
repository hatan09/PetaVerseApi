namespace PetaVerseApi.Core.Entities
{
    public class PetShorts : BaseEntity
    {
        public string         Title     { get; set; } = String.Empty;
        public int            Like      { get; set; }
        public bool           IsSpam    { get; set; }
        public PetaverseMedia Media     { get; set; }
        public Animal         Pet       { get; set; }
        public User           Publisher { get; set; }
    }
}
