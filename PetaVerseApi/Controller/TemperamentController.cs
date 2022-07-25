using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.DTOs;

namespace PetaVerseApi.Controller
{
    public class TemperamentController : ControllerBase
    {
        private readonly ITemperamentRepository _temperamentRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public TemperamentController(IMapper mapper, ITemperamentRepository temperamentRepository, IUserRepository userRepsitory)
        {
            _mapper = mapper;
            _temperamentRepository = temperamentRepository;
            _userRepository = userRepsitory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var breed = await _temperamentRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<TemperamentDTO>>(breed));
        }


    }
}
