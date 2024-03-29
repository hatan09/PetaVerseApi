﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetaVerseApi.AppSettings;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using PetaVerseApi.DTOs.Create;
using PetaVerseApi.Interfaces;
using PetaVerseApi.Services;
using MediaType = PetaVerseApi.Core.Entities.MediaType;

namespace PetaVerseApi.Controller
{
    public class AnimalController : BaseController
    {
        private readonly IMapper                             _mapper;
        private readonly IMediaService                       _mediaService;
        private readonly AnimalService                       _animalService;
        private readonly IUserRepository                     _userRepository;
        private readonly IBreedRepository                    _breedRepository;
        private readonly IAnimalRepository                   _animalRepository;
        private readonly ISpeciesRepository                  _speciesRepository;
        private readonly ApplicationDbContext                _petaverseDbContext;
        private readonly IUserAnimalRepository               _userAnimalRepository;
        private readonly IPetaverseMediaRepository           _petaverseMediaRepository;
        private readonly IAnimalPetaverseMediaRepository     _animalPetaverseMediaRepository;
        private readonly IOptionsMonitor<AzureStorageConfig> _azureStorageConfig;

        public AnimalController(IMapper mapper,
                                IMediaService mediaService, 
                                AnimalService animalService,
                                IUserRepository userRepository,
                                IBreedRepository breedRepository,
                                IAnimalRepository animalRepository,
                                ISpeciesRepository speciesRepository,
                                ApplicationDbContext petaverseDpContext,
                                IUserAnimalRepository userAnimalRepository,
                                IPetaverseMediaRepository petaverseMediaRepository,
                                IAnimalPetaverseMediaRepository animalPetaverseMediaRepository,
                                IOptionsMonitor<AzureStorageConfig> azureStorageConfig)
        {
            _mapper                         = mapper;
            _mediaService                   = mediaService;
            _animalService                  = animalService;
            _userRepository                 = userRepository;
            _breedRepository                = breedRepository;
            _animalRepository               = animalRepository;
            _speciesRepository              = speciesRepository;
            _azureStorageConfig             = azureStorageConfig;
            _petaverseDbContext             = petaverseDpContext;
            _userAnimalRepository           = userAnimalRepository;   
            _petaverseMediaRepository       = petaverseMediaRepository;
            _animalPetaverseMediaRepository = animalPetaverseMediaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var animals = await _animalRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<AnimalDTO>>(animals));
        }


        [HttpGet("{animalId}")]
        public async Task<IActionResult> GetById(int animalId, CancellationToken cancellationToken = default)
        {
            var animal = await _animalRepository.FindByIdAsync(animalId, cancellationToken);
            return animal != null ? Ok(_mapper.Map<AnimalDTO>(animal)) : NotFound("Unable to find the requested animal"); 
        }

        [HttpGet("{userGuid}")]
        public async Task<IActionResult> GetAllByUserGuid(string userGuid, CancellationToken cancellationToken = default)
        {
            //Check if user exist
            var user = await _userRepository.FindByGuidAsync(userGuid, cancellationToken);
            if(user != null){
                //Get AnimalID
                var animalIds = await _userAnimalRepository.FindAll(ua => ua.UserId == user.Id).Select(ua => ua.AnimalId).ToListAsync();
                var animalsDTO = new List<AnimalDTO>();
                foreach (var id in animalIds)
                {
                    var animal = await _animalRepository.FindAnimalWithFullInfo(id, cancellationToken);
                    if (animal != null)
                    {
                        var petPhotoIds = _animalPetaverseMediaRepository.FindAll(apm => apm.AnimalId == id).Select(apm => apm.PetaverMediaId).ToList();
                        var petPhotos = new List<PetaverseMediaDTO>();
                        foreach (var petPhotoId in petPhotoIds)
                        {
                            //media => media.Id == id && media.Type == MediaType.Photo
                            var photo = await _petaverseMediaRepository.FindByIdAsync(petPhotoId);
                            if (photo != null)
                                petPhotos.Add(_mapper.Map<PetaverseMediaDTO>(photo));
                        };
                        var animalDTO = _mapper.Map<AnimalDTO>(animal);
                        animalDTO.PetPhotos = petPhotos;

                        animalsDTO.Add(animalDTO);
                    }
                }
                return Ok(animalsDTO);
            }
            else return NotFound("User invalid");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetDTO dto, CancellationToken cancellationToken = default)
        {
            var listOfOwnersGuid = dto.OwnerGuids.Split(',').ToList();
            var listOfOwner = _userRepository.FindAll(u => listOfOwnersGuid.Any(i => i == u.Guid));
            var excludeGuids = listOfOwnersGuid.Except(listOfOwner.Select(u => u.Guid));
            if(excludeGuids.Any())
            {
                return NotFound($"Cant find user with Guid: {String.Join(", ", excludeGuids)}");
            }
            using var petaverseTransaction = await _petaverseDbContext.Database.BeginTransactionAsync();
            var animal = _mapper.Map<Animal>(dto);
            if (animal != null)
            {
                var breed = await _breedRepository.FindByIdIQueryable(dto.BreedId, cancellationToken).FirstOrDefaultAsync();

                if (breed != null)
                {
                    animal.SixDigitCode = await _animalRepository.Generate6DigitCodeAsync();
                    animal.BreedId = breed.Id;
                    _animalRepository.Add(animal);
                    await _animalRepository.SaveChangesAsync(cancellationToken);
                }
                else return NotFound("Can't find this breed is our source !!");

                if (listOfOwner.ToList().Count > 0)
                {
                    listOfOwner.ToList().ForEach(owner => _userAnimalRepository.Add(new UserAnimal() { UserId = owner.Id, AnimalId = animal.Id }));
                    await _userAnimalRepository.SaveChangesAsync(cancellationToken);
                }

                await petaverseTransaction.CommitAsync(cancellationToken);
                return Ok(animal.Id);
            }
            else return BadRequest("Can't converter request to animal");
        }

        [HttpPost("{petId}")]
        public async Task<IActionResult> UploadPetAvatar(int petId, IFormFile avatar, CancellationToken cancellationToken)
        {
            var pet = await _animalRepository.FindByIdAsync(petId, cancellationToken);
            if (pet == null)
                return NotFound("Not Found This Pet");
            var petaverMediaDTO = await _animalService.UploadAnimalPhotoAsync(pet, 
                                                                              avatar, 
                                                                              MediaType.Avatar, 
                                                                              _azureStorageConfig.CurrentValue.PetaversePetAvatars,
                                                                              cancellationToken);
            return petaverMediaDTO is null 
                    ? BadRequest("Can't create avatar") 
                    : Ok(petaverMediaDTO);
        }

        [HttpPost("{petId}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAnimalMedias(int petId, IFormFileCollection medias, CancellationToken cancellationToken)
        {
            var pet = await _animalRepository.FindByIdAsync(petId, 
                                                            cancellationToken);
            if(pet == null)
                return NotFound("Not Found This Pet");
            else
            {
                if (medias.Count == 0)
                    return BadRequest("No medias received from the upload");
                return Ok(await _animalService.UploadAnimalPhotosAsync(pet, 
                                                                       medias, 
                                                                       cancellationToken));
            }
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
            var userAnimals = _userAnimalRepository.FindAll(ua => ua.AnimalId == animal.Id).ToList();
            userAnimals.ForEach(ua => _userAnimalRepository.Delete(ua));

            var animalPetaverseMedias = await _animalPetaverseMediaRepository.FindAll(apm => apm.AnimalId == id)
                                                                             .ToListAsync(cancellationToken);



            foreach (var animalPetaverseMedia in animalPetaverseMedias)
            {
                var petaverseMedia = await _petaverseMediaRepository.FindByIdAsync(animalPetaverseMedia.PetaverMediaId);
                if(petaverseMedia is not null)
                {
                    await _mediaService.DeleteFileAsync(petaverseMedia.MediaName, 
                                                        petaverseMedia.Type);

                    _petaverseMediaRepository.Delete(petaverseMedia);
                    _animalPetaverseMediaRepository.Delete(animalPetaverseMedia);
                }
                
            }

            await _animalRepository.SaveChangesAsync(cancellationToken);
            await _userAnimalRepository.SaveChangesAsync(cancellationToken);
            await _petaverseMediaRepository.SaveChangesAsync(cancellationToken);
            await _animalPetaverseMediaRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
