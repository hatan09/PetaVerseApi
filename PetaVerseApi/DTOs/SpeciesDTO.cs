using Microsoft.AspNetCore.Mvc;
using PetaVerseApi.Services;

namespace PetaVerseApi.DTOs
{
    //[ModelBinder(typeof(MultipleSourcesModelBinder<BreedDTO>))]
    public class SpeciesDTO : BaseDTO
    {
        public string   Name            { get; set; } = string.Empty;
        public string   Description     { get; set; } = string.Empty;

        public virtual ICollection<int>     Breeds     { get; set; } = Array.Empty<int>();
        public virtual ICollection<int>     Animals    { get; set; } = Array.Empty<int>();
    }
}
