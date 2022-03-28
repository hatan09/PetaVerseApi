using Microsoft.AspNetCore.Mvc;
using PetaVerseApi.Services;

namespace PetaVerseApi.DTOs
{
    //[ModelBinder(typeof(MultipleSourcesModelBinder<BreedDTO>))]
    public class SpeciesDTO : BaseDTO
    {
        public string   Name            { get; set; } = string.Empty;
        public string   Description     { get; set; } = string.Empty;

        public ICollection<string>   Breeds     { get; set; } = Array.Empty<string>(); 
    }
}
