using Microsoft.AspNetCore.Mvc;
using PetaVerseApi.Services;
using System.ComponentModel.DataAnnotations;

namespace PetaVerseApi.DTOs
{
    [ModelBinder(typeof(MultipleSourcesModelBinder<RoleDTO>))]
    public class RoleDTO : BaseDTO<string>
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
