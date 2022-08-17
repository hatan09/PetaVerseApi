namespace PetaVerseApi.DTOs.Create
{
    public class CreatePetDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public string? PetColor { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;
        public string OwnerGuids { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double Age { get; set; }
        public int BreedId { get; set; }
    }
}
