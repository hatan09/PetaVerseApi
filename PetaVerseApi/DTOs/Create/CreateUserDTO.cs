using Microsoft.AspNetCore.Mvc;
using PetaVerseApi.Services;
using System.ComponentModel.DataAnnotations;

namespace PetaVerseApi.DTOs.Create
{
    [ModelBinder(typeof(MultipleSourcesModelBinder<CreateUserDTO>))]
    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public IList<string> Roles { get; set; } = Array.Empty<string>();
    }
}
