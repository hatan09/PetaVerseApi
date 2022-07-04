using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;

namespace PetaVerseApi.Controller
{
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IBreedRepository _breedRepository;

        public AnimalController(IAnimalRepository animalRepository, ISpeciesRepository speciesRepository, IBreedRepository breedRepository, IMapper mapper)
        {
            _animalRepository = animalRepository;
            _speciesRepository = speciesRepository;
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var breed = await _animalRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<AnimalDTO>>(breed));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalDTO dto, CancellationToken cancellationToken = default)
        {
            var animal = _mapper.Map<Animal>(dto);
            _animalRepository.Add(animal);

            await _animalRepository.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map<AnimalDTO>(animal));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] BreedDTO dto, int id, CancellationToken cancellationToken = default)
        {
            var breed = await _animalRepository.FindByIdAsync(id, cancellationToken);
            if (breed is null)
                return NotFound();

            _mapper.Map(dto, breed);
            await _animalRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var animal = await _animalRepository.FindByIdAsync(id, cancellationToken);
            if (animal is null)
                return NotFound();

            _animalRepository.Delete(animal);
            await _animalRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
