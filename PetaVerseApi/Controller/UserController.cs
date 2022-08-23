﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;

namespace PetaVerseApi.Controller
{
    public class UserController : BaseController
    {
        private readonly IMapper                         _mapper;
        private readonly IUserRepository                 _userRepository;
        private readonly IAnimalRepository               _animalRepository;
        private readonly IUserAnimalRepository           _userAnimalRepository;
        private readonly IAnimalPetaverseMediaRepository _animalPetaverseMediaRepository;

        public UserController(IMapper mapper, 
                              IUserRepository userRepository, 
                              IAnimalRepository animalRepository, 
                              IUserAnimalRepository userAnimalRepository,
                              IAnimalPetaverseMediaRepository animalPetaverseMediaRepository)
        {
            _mapper                         = mapper;
            _userRepository                 = userRepository;
            _animalRepository               = animalRepository;
            _userAnimalRepository           = userAnimalRepository;
            _animalPetaverseMediaRepository = animalPetaverseMediaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(user));
        }

        [HttpGet("{userGuid}")]
        public async Task<IActionResult> GetByUserGuid(string userGuid, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByGuidAsync(userGuid, cancellationToken);
            return user != null ? Ok(_mapper.Map<UserDTO>(user)) : NotFound("Unable to find the requested user");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindByGuidAsync(userDTO.Guid);
            if (existingUser is null)
            {
                _userRepository.Add(_mapper.Map<User>(userDTO));
                await _userRepository.SaveChangesAsync(cancellationToken);
                return Ok(userDTO.Guid);
            }
            else return BadRequest("This user is already exist !");
        }
    }
}
