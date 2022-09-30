using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using PetaVerseApi.Interfaces;
using ExcelDataReader;
using MediaType = PetaVerseApi.Core.Entities.MediaType;
using Microsoft.Extensions.Options;
using PetaVerseApi.AppSettings;
using PetaVerseApi.Services;

namespace PetaVerseApi.Controller
{
    public class BreedController : BaseController
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IExcelHandlerService _excelHandlerService;
        private readonly IOptionsMonitor<AzureStorageConfig> _storageConfig;
        private readonly AnimalService _animalService;

        public BreedController(IMapper                             mapper,
                               IMediaService                       mediaService,
                               AnimalService                       animalService,
                               IBreedRepository                    breedRepository,
                               IAnimalRepository                   animalRepository, 
                               ISpeciesRepository                  speciesRepository,
                               IExcelHandlerService                excelHandlerService,
                               IOptionsMonitor<AzureStorageConfig> storageConfig)
        {
            _mapper              = mapper;
            _mediaService        = mediaService;
            _storageConfig       = storageConfig;
            _animalService       = animalService;
            _breedRepository     = breedRepository;
            _animalRepository    = animalRepository;
            _speciesRepository   = speciesRepository;
            _excelHandlerService = excelHandlerService;
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

        [HttpGet("{speciesId}")]
        public async Task<IActionResult> GetBySpeciesId(int speciesId, CancellationToken cancellationToken)
        {
            var breeds = await _breedRepository.GetBySpeciesId(speciesId, cancellationToken);
            return Ok(_mapper.Map<IEnumerable<BreedDTO>>(breeds));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BreedDTO dto, CancellationToken cancellationToken = default)
        {
            var breed = _mapper.Map<Breed>(dto);
            _breedRepository.Add(breed);

            await _breedRepository.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map<BreedDTO>(breed));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UploadBreedImage(int id, IFormFile image, CancellationToken cancellationToken = default)
        {
            var breed = await _breedRepository.FindByIdAsync(id, cancellationToken);
            if (breed is null)
                return NotFound($"No Breed With Id {id} Found!");
            else
            {
                return Ok(await _animalService.UploadFileAsync(image, MediaType.Photo, 
                                                               _storageConfig.CurrentValue.PetaverseGeneralFile,
                                                               cancellationToken));
            }
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

        [HttpPost("{speciesId}")]
        public async Task<IActionResult> UploadDataFromExcel(int speciesId, 
                                                             IFormFile file, 
                                                             CancellationToken cancellationToken)
        {            
            var rowCollection = await _excelHandlerService.GetRows(file, cancellationToken);

            for (var i = 0; i < rowCollection.Count; i++)
            {
                var imageUrl = rowCollection[i][6].ToString()!;
                if(!string.IsNullOrEmpty(imageUrl) && !string.IsNullOrWhiteSpace(imageUrl) && speciesId != 0)
                {
                    BreedDTO dto = new()
                    {
                        BreedName = rowCollection[i][0].ToString()!,
                        Color = rowCollection[i][1].ToString()!,
                        ImageUrl = imageUrl,
                        BreedDescription = rowCollection[i][3].ToString()!,
                        SpeciesId = speciesId,
                    };

                    var breed = _mapper.Map<Breed>(dto);
                    _breedRepository.Add(breed);
                }
            }

            await _breedRepository.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
