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
    public class PetaverseMediaController : ControllerBase
    {
        private readonly IPetaverseMediaRepository _petaverseMediaRepository;
        private readonly IMapper _mapper;

        public PetaverseMediaController(IPetaverseMediaRepository petaverseMediaRepository, IMapper mapper)
        {
            _petaverseMediaRepository = petaverseMediaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var media = await _petaverseMediaRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<PetaverseMediaDTO>>(media));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PetaverseMediaDTO dto, CancellationToken cancellationToken = default)
        {
            var media = _mapper.Map<PetaverseMedia>(dto);
            _petaverseMediaRepository.Add(media);

            await _petaverseMediaRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PetaverseMediaDTO dto, CancellationToken cancellationToken = default)
        {
            var media = await _petaverseMediaRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (media is null)
                return NotFound();

            _mapper.Map(dto, media);
            await _petaverseMediaRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var media = await _petaverseMediaRepository.FindByIdAsync(id, cancellationToken);
            if (media is null)
                return NotFound();

            _petaverseMediaRepository.Delete(media);
            await _petaverseMediaRepository.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
