using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using PetaVerseApi.DTOs.Create;
using PetaVerseApi.Repository;

namespace PetaVerseApi.Controller
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager userManager, IMapper mapper, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                _logger.LogError("Unable to create user {username}. Result details: {result}", dto.Username, string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                return BadRequest(result);
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByGuidAsync(dto.Guid);
            if (user == null || user.IsDeleted)
                return NotFound();

            _mapper.Map(dto, user);
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string guid)
        {
            var user = await _userManager.FindByIdAsync(guid);
            if (user == null || user.IsDeleted)
                return NotFound();

            user.IsDeleted = true;
            await _userManager.DeleteAsync(user);
            return NoContent();
        }
    }
}
