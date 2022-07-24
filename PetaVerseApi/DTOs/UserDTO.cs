using Microsoft.AspNetCore.Mvc;
using PetaVerseApi.Services;
using System.ComponentModel.DataAnnotations;

namespace PetaVerseApi.DTOs
{
    //[ModelBinder(typeof(MultipleSourcesModelBinder<UserDTO>))]
    public class UserDTO
    {
        [FromRoute]
        public string       Guid                { get; set; } = string.Empty;

        [Phone]
        public string?      PhoneNumber         { get; set; }
        public string?      ProfileImageUrl     { get; set; }
        public string?      CoverImageUrl       { get; set; }
        public DateTime     CreatedAt           { get; set; } = DateTime.UtcNow;
        public bool?        IsActive            { get; set; }
        public bool         IsDeleted           { get; set; } = false;

        public ICollection<string>  Roles   { get; set; } = Array.Empty<string>();
    }
}
