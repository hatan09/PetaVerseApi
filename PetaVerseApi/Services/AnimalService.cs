using AutoMapper;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PetaVerseApi.AppSettings;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using PetaVerseApi.Interfaces;
using System.Threading;
using MediaType = PetaVerseApi.Core.Entities.MediaType;

namespace PetaVerseApi.Services
{
    public class AnimalService
    {
        private readonly IMapper                             _mapper;
        private readonly IMediaService                       _mediaService;
        private readonly IUserRepository                     _userRepository;
        private readonly IBreedRepository                    _breedRepository;
        private readonly IAnimalRepository                   _animalRepository;
        private readonly ISpeciesRepository                  _speciesRepository;
        private readonly ApplicationDbContext                _petaverseDbContext;
        private readonly IUserAnimalRepository               _userAnimalRepository;
        private readonly IPetaverseMediaRepository           _petaverseMediaRepository;
        private readonly IAnimalPetaverseMediaRepository     _animalPetaverseMediaRepository;
        private readonly IOptionsMonitor<AzureStorageConfig> _azureStorageConfig;
        public AnimalService(IMapper mapper,
                             IMediaService mediaService,
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
            _mapper = mapper;
            _mediaService = mediaService;
            _userRepository = userRepository;
            _breedRepository = breedRepository;
            _animalRepository = animalRepository;
            _speciesRepository = speciesRepository;
            _azureStorageConfig = azureStorageConfig;
            _petaverseDbContext = petaverseDpContext;
            _userAnimalRepository = userAnimalRepository;
            _petaverseMediaRepository = petaverseMediaRepository;
            _animalPetaverseMediaRepository = animalPetaverseMediaRepository;
        }

        public async Task<List<PetaverseMediaDTO>> UploadAnimalPhotosAsync(Animal animal,
                                                                           IFormFileCollection medias,
                                                                           CancellationToken cancellationToken)
        {
            var uploadedPetPhotos = new List<PetaverseMediaDTO>();
            foreach (var formFile in medias)
            {
                using (Stream stream = formFile.OpenReadStream())
                {
                    var petaverseMedia = await _mediaService.UploadFileToStorage(stream, 
                                                                                 animal.SixDigitCode + "_" + formFile.FileName,
                                                                                 MediaType.Photo,
                                                                                 _azureStorageConfig.CurrentValue.PetaverseGallery);
                    if (petaverseMedia is not null)
                    {

                        _petaverseMediaRepository.Add(petaverseMedia);
                        await _petaverseMediaRepository.SaveChangesAsync(cancellationToken);

                        _animalPetaverseMediaRepository.Add(new AnimalPetaverseMedia()
                        {
                            AnimalId = animal.Id,
                            PetaverMediaId = petaverseMedia.Id
                        });
                        await _animalPetaverseMediaRepository.SaveChangesAsync(cancellationToken);

                        uploadedPetPhotos.Add(new PetaverseMediaDTO()
                        {
                            Id = petaverseMedia.Id,
                            MediaUrl = petaverseMedia.MediaUrl,
                            Type = DTOs.MediaType.Photo
                        });
                    }
                }
            }
            return uploadedPetPhotos;
        }

        public async Task<PetaverseMediaDTO?> UploadFileAsync(IFormFile file,
                                                              MediaType type,
                                                              string containerName,
                                                              CancellationToken cancellationToken)
        {
            using (Stream stream = file.OpenReadStream())
            {
                var petaverseMedia = await _mediaService.UploadFileToStorage(stream, 
                                                                             file.FileName,
                                                                             type,
                                                                             containerName);
                if (petaverseMedia is not null)
                {
                    _petaverseMediaRepository.Add(petaverseMedia);
                    await _petaverseMediaRepository.SaveChangesAsync(cancellationToken);
                    return _mapper.Map<PetaverseMediaDTO>(petaverseMedia);
                }
                else return null;
            }
        }

        public async Task<PetaverseMediaDTO?> UploadAnimalPhotoAsync(Animal animal,
                                                                     IFormFile file,
                                                                     MediaType type,
                                                                     string containerName,
                                                                     CancellationToken cancellationToken)
        {
            using (Stream stream = file.OpenReadStream())
            {
                var petaverseMedia = await _mediaService.UploadFileToStorage(stream, 
                                                                             animal.SixDigitCode + "_" + file.FileName,
                                                                             type, 
                                                                             containerName);
                if (petaverseMedia is not null)
                {
                    _petaverseMediaRepository.Add(petaverseMedia);
                    await _petaverseMediaRepository.SaveChangesAsync(cancellationToken);

                    animal.PetAvatarId = petaverseMedia.Id;
                    await _animalRepository.SaveChangesAsync(cancellationToken);

                    _animalPetaverseMediaRepository.Add(new AnimalPetaverseMedia()
                    {
                        AnimalId = animal.Id,
                        PetaverMediaId = petaverseMedia.Id
                    });
                    await _animalPetaverseMediaRepository.SaveChangesAsync(cancellationToken);
                    return _mapper.Map<PetaverseMediaDTO>(animal.PetAvatar);
                }
                else return null;
            }
        }
    }
}
