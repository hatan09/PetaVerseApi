﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;

namespace PetaVerseApi.Controller
{
    public class PetShortsController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IPetaverseMediaRepository _mediaRepository;
        private readonly IPetShortsRepository _petShortsRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;

        public PetShortsController(IAnimalRepository animalRepository, IUserRepository userRepository, IPetShortsRepository petShortsRepository, IPetaverseMediaRepository petaverseMediaRepository, IMapper mapper)
        { 
            _animalRepository = animalRepository;
            _userRepository = userRepository;
            _petShortsRepository = petShortsRepository;
            _mediaRepository = petaverseMediaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var breed = await _petShortsRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<PetShortsDTO>>(breed));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PetShortsDTO dto, CancellationToken cancellationToken = default)
        {
            var petShorts = _mapper.Map<PetShorts>(dto);
            _petShortsRepository.Add(petShorts);

            await _petShortsRepository.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map<PetShortsDTO>(petShorts));
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo(int petShortId, 
                                                     IFormFile video, 
                                                     CancellationToken cancellationToken)
        {
            var petShort = await _petShortsRepository.FindByIdAsync(petShortId, cancellationToken);
            if (petShort == null)
                return NotFound("Not Found This Short");
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] PetShortsDTO dto, CancellationToken cancellationToken = default)
        {
            var petShorts = await _petShortsRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (petShorts is null)
                return NotFound();

            _mapper.Map(dto, petShorts);
            await _petShortsRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var petShorts = await _petShortsRepository.FindByIdAsync(id, cancellationToken);
            if (petShorts is null)
                return NotFound();

            _petShortsRepository.Delete(petShorts);
            await _petShortsRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
