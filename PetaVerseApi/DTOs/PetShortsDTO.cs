namespace PetaVerseApi.DTOs
{
    public class PetShortsDTO : BaseDTO 
    {
        public string Title { get; set; } = String.Empty;
        public int Like { get; set; }
        public bool IsSpam { get; set; }
    }
}
