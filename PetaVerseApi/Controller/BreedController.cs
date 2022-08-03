using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;

namespace PetaVerseApi.Controller
{
    public class BreedController : BaseController
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IAnimalRepository _animalRepository;

        public BreedController(ISpeciesRepository speciesRepository, 
                               IBreedRepository breedRepository,
                               IMapper mapper,
                               IAnimalRepository animalRepository)
        {
            _speciesRepository = speciesRepository;
            _breedRepository = breedRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var breeds = await _breedRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<BreedDTO>>(breeds));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var breed = await _breedRepository.FindByIdAsync(id, cancellationToken);
            return Ok(_mapper.Map<BreedDTO>(breed));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BreedDTO dto, CancellationToken cancellationToken = default)
        {
            var breed = _mapper.Map<Breed>(dto);

            foreach (var animals in dto.Animals)
            {
                var foundAnimal = await _animalRepository.FindByIdAsync(dto.Id, cancellationToken);
                if (foundAnimal is null)
                    return NotFound($"AuthorGuid {animals} not found");

                breed.Animals.Add(foundAnimal);
            }
            _breedRepository.Add(breed);

            await _breedRepository.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map<BreedDTO>(breed));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] BreedDTO dto, CancellationToken cancellationToken = default)
        {
            var breed = await _breedRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (breed is null)
                return NotFound();

            _mapper.Map(dto, breed);
            await _breedRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var breed = await _breedRepository.FindByIdAsync(id, cancellationToken);
            if (breed is null)
                return NotFound();

            _breedRepository.Delete(breed);
            await _breedRepository.SaveChangesAsync(cancellationToken); 
            return NoContent();
        }
    }
}
