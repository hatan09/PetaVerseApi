using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;

namespace PetaVerseApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;
        private readonly ISpeciesRepository _speciesRepository;

        public BreedController(ISpeciesRepository speciesRepository, IBreedRepository breedRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var breed = await _breedRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<BreedDTO>>(breed));
        }

        [HttpPost]
        public async Task<IActionResult> Create(BreedDTO dto, CancellationToken cancellationToken = default)
        {
            var breed = _mapper.Map<Breed>(dto);
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
