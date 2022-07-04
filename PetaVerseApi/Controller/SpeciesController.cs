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
        private readonly IAnimalRepository _animalRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public SpeciesController(IAnimalRepository animalRepository, ISpeciesRepository speciesRepository, IBreedRepository breedRepository, IMapper mapper)
        {
            _animalRepository = animalRepository;
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
            foreach (var animals in dto.Animals)
            {
                var foundAnimal = await _animalRepository.FindByIdAsync(dto.Id, cancellationToken);
                if (foundAnimal is null)
                    return NotFound($"AuthorGuid {animals} not found");

                species.Animals.Add(foundAnimal);
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
            ICollection<Animal> animals = species.Animals;
            //want to delete
            ICollection<int> requestBreeds = dto.Breeds;
            ICollection<int> requestAnimals = dto.Animals;
            //original one
            ICollection<int> originalBreeds = species.Breeds.Select(s => s.Id).ToList();
            ICollection<int> originalAnimals = species.Animals.Select(s => s.Id).ToList();
            //deleted
            ICollection<int> deleteBreeds = originalBreeds.Except(requestBreeds).ToList();
            ICollection<int> deleteAnimals = originalAnimals.Except(requestAnimals).ToList();

            // Delete from species
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

            if (deleteAnimals.Count > 0)
            {
                foreach (var animal in deleteAnimals)
                {
                    var foundAnimal = await _animalRepository.FindByIdAsync(dto.Id, cancellationToken);
                    if (foundAnimal is null)
                        return NotFound($"AuthorGuid {animals} not found");

                    animals.Remove(foundAnimal);
                }
            }

            // Add new one to species
            ICollection<int> newBreeds = requestBreeds.Except(originalBreeds).ToList();
            if (newBreeds.Count > 0)
            {
                foreach (var breed in newBreeds)
                {
                    var foundBreed = await _breedRepository.FindByIdAsync(dto.Id, cancellationToken);
                    if (foundBreed is null)
                        return NotFound($"AuthorGuid {breeds} not found");

                    breeds.Add(foundBreed);
                }
            }

            ICollection<int> newAnimals = requestAnimals.Except(originalAnimals).ToList();
            if (newAnimals.Count > 0)
            {
                foreach (var animal in newAnimals)
                {
                    var foundAnimal = await _animalRepository.FindByIdAsync(dto.Id, cancellationToken);
                    if (foundAnimal is null)
                        return NotFound($"AuthorGuid {animals} not found");

                    animals.Add(foundAnimal);
                }
            }

            await _speciesRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnimal([FromBody] SpeciesDTO dto, CancellationToken cancellationToken = default)
        {
            var species = await _speciesRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (species is null)
                return NotFound();

            ICollection<Animal> animals = species.Animals;
            ICollection<int> requestAnimals = dto.Animals;
            ICollection<int> originalAnimals = species.Animals.Select(s => s.Id).ToList();
            ICollection<int> deleteAnimals = originalAnimals.Except(requestAnimals).ToList();

            if (deleteAnimals.Count > 0)
            {
                foreach (var animal in deleteAnimals)
                {
                    var foundAnimal = await _animalRepository.FindByIdAsync(dto.Id, cancellationToken);
                    if (foundAnimal is null)
                        return NotFound($"AuthorGuid {animals} not found");

                    animals.Remove(foundAnimal);
                }
            }

            ICollection<int> newAnimals = requestAnimals.Except(originalAnimals).ToList();
            if (newAnimals.Count > 0)
            {
                foreach (var animal in newAnimals)
                {
                    var foundAnimal = await _animalRepository.FindByIdAsync(dto.Id, cancellationToken);
                    if (foundAnimal is null)
                        return NotFound($"AuthorGuid {animals} not found");

                    animals.Add(foundAnimal);
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
