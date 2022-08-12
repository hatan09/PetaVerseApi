using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using PetaVerseApi.Interfaces;
using ExcelDataReader;


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

        public BreedController(ISpeciesRepository speciesRepository,
                               IMediaService mediaService,
                               IBreedRepository breedRepository,
                               IMapper mapper,
                               IAnimalRepository animalRepository,
                               IExcelHandlerService excelHandlerService)
        {
            _speciesRepository = speciesRepository;
            _mediaService = mediaService;
            _breedRepository = breedRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
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

            foreach (var animals in dto.Animals)
            {
                var foundAnimal = await _animalRepository.FindByIdAsync(dto.Id, cancellationToken);
                if (foundAnimal is null)
                    return NotFound($"AuthorGuid {animals} not found");

                breed.Animals.Add(foundAnimal);
            }
            _breedRepository.Add(breed);

            await _breedRepository.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map<BreedDTO>(breed));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UploadBreeadImage(int id, IFormFile image, CancellationToken cancellationToken = default)
        {
            if (_mediaService.IsImage(image))
            {
                var breed = await _breedRepository.FindByIdAsync(id, cancellationToken);
                if (breed is null)
                {
                    return NotFound($"No Breed With Id {id} Found!");
                }
                using (Stream stream = image.OpenReadStream())
                {
                    Tuple<bool, string> result = await _mediaService.UploadAvatarToStorage(stream, image.FileName);
                    var isUploaded = result.Item1;
                    var stringUrl = result.Item2;
                    if (isUploaded && !string.IsNullOrEmpty(stringUrl))
                    {
                        breed.ImageUrl = stringUrl;
                        await _breedRepository.SaveChangesAsync(cancellationToken);

                        return Ok(stringUrl);
                    }
                    else return BadRequest("Look like the image couldnt upload to the storage");
                }
            }
            else return new UnsupportedMediaTypeResult();
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

        [HttpPost]
        public async Task<IActionResult> UploadDataFromExcel(IFormFile file, CancellationToken cancellationToken)
        {            
            var rowCollection = await _excelHandlerService.GetRows(file, cancellationToken);

            for (var i = 0; i < rowCollection.Count; i++)
            {
                var imageUrl = rowCollection[i][6].ToString()!;
                if(!string.IsNullOrEmpty(imageUrl) && !string.IsNullOrWhiteSpace(imageUrl))
                {
                    BreedDTO dto = new()
                    {
                        BreedName = rowCollection[i][0].ToString()!,
                        Color = rowCollection[i][1].ToString()!,
                        ImageUrl = rowCollection[i][6].ToString()!,
                        BreedDescription = rowCollection[i][3].ToString()!,
                        SpeciesId = 1,
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
