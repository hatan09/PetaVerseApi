using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;

namespace PetaVerseApi.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;
        private readonly ISpeciesRepository _speciesRepository;

        public SpeciesController(ISpeciesRepository speciesRepository, IBreedRepository breedRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var species = await _speciesRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<SpeciesDTO>>(species));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var species = await _speciesRepository.FindByIdAsync(id);
            if (species is null)
                return NotFound();

            return Ok(_mapper.Map<SpeciesDTO>(species));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SpeciesDTO dto, CancellationToken cancellationToken = default)
        {
            var species = _mapper.Map<Species>(dto);

            foreach (var breeds in dto.Breeds)
            {
                var foundBreed = await _breedRepository.FindByIdAsync(dto.Id, cancellationToken);
                if (foundBreed is null)
                    return NotFound($"AuthorGuid {breeds} not found");

                species.Breeds.Add(foundBreed);
            }
            _speciesRepository.Add(species);

            await _speciesRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { species.Id }, _mapper.Map<SpeciesDTO>(species));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]  SpeciesDTO dto, CancellationToken cancellationToken = default)
        {
            var species = await _speciesRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (species is null)
                return NotFound();

            _mapper.Map(dto, species);

            ICollection<Breed> breeds = species.Breeds;
            ICollection<int> requestBreeds = dto.Breeds;
            ICollection<int> originalBreeds = species.Breeds.Select(s => s.Id).ToList();

            // Delete breed from species
            ICollection<int> deleteBreeds = originalBreeds.Except(requestBreeds).ToList();
            if (deleteBreeds.Count > 0)
            {
                foreach (var breed in deleteBreeds)
                {
                    var foundBreed = await _breedRepository.FindByIdAsync(dto.Id, cancellationToken);
                    if (foundBreed is null)
                        return NotFound($"AuthorGuid {breeds} not found");

                    breeds.Remove(foundBreed);
                }
            }

            // Add new breed to species
            ICollection<int> newBreeds = requestBreeds.Except(originalBreeds).ToList();
            if (newBreeds.Count > 0)
            {
                foreach (var breed in newBreeds)
                {
                    var foundBreed = await _breedRepository.FindByIdAsync(dto.Id, cancellationToken);
                    if (foundBreed is null)
                        return NotFound($"AuthorGuid {breed} not found");

                    breeds.Add(foundBreed);
                }
            }

            await _speciesRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var species = await _speciesRepository.FindByIdAsync(id, cancellationToken);
            if (species is null)
                return NotFound();

            _speciesRepository.Delete(species);
            await _speciesRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
