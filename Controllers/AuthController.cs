using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orange_Portfolio_BackEnd.Application.Services;
using Orange_Portfolio_BackEnd.Application.ViewModel;
using Orange_Portfolio_BackEnd.Domain.DTOs;
using Orange_Portfolio_BackEnd.Domain.Models;
using Orange_Portfolio_BackEnd.Domain.Models.Interfaces;

namespace Orange_Portfolio_BackEnd.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, TokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            // Check if the email is already in use
            if (await _userRepository.GetByEmail(model.Email) != null)
            {
                return BadRequest("This email is already in use.");
            }

            var passwordHash = CryptographyService.Encrypt(model.Password);

            // Create a new user
            var newUser = new User
                (
                model.Name,
                model.LastName,
                model.Email,
                passwordHash
            );

            await _userRepository.Add(newUser);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var user = await _userRepository.GetByEmail(model.Email);

            if (user == null)
            {
                return NotFound("This email is not registered!");
            }

            if (CryptographyService.Verify(model.Password, user.Password))
            {
                var token = _tokenService.GenerateToken(user);
                var userDTO = _mapper.Map<UserDTO>(user);

                object response = new
                {
                    User = userDTO,
                    Token = token
                };

                return Ok(response);
            }

            return BadRequest("Invalid password!");
        }
    }
}
