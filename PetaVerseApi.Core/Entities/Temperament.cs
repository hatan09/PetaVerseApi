namespace PetaVerseApi.Core.Entities
{
    public class Temperament : BaseEntity
    {
        public string Info  { get; set; } = String.Empty;
        public int    Level { get; set; }
    }
}
