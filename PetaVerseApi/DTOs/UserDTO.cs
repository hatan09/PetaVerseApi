using Microsoft.AspNetCore.Mvc;

namespace PetaVerseApi.DTOs
{
    //[ModelBinder(typeof(MultipleSourcesModelBinder<UserDTO>))]
    public class UserDTO
    {
        [FromRoute]
        public string       Guid                     { get; set; } = string.Empty;
        public string?      PetaverseProfileImageUrl { get; set; }
        public string?      CoverImageUrl            { get; set; }
        public DateTime     CreatedAt                { get; set; } = DateTime.UtcNow;
        public bool?        IsActive                 { get; set; }
        public bool         IsDeleted                { get; set; } = false;

        public ICollection<string>  Roles   { get; set; } = Array.Empty<string>();
    }

    public class UserWithAnimalDTO : UserDTO
    {
        public ICollection<AnimalDTO> Pets { get; set; } = Array.Empty<AnimalDTO>();
    }
}
