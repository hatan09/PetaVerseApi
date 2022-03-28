using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;

namespace PetaVerseApi.Controller
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> Create(SpeciesDTO dto, CancellationToken cancellationToken = default)
        {
            var species = _mapper.Map<Species>(dto);
            _speciesRepository.Add(species);

            await _speciesRepository.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map<SpeciesDTO>(species));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]  SpeciesDTO dto, int id, CancellationToken cancellationToken = default)
        {
            var species = await _speciesRepository.FindByIdAsync(id,cancellationToken);
            if (species is null)
                return NotFound();

            _mapper.Map(dto, species);
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
