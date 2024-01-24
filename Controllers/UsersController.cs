using Microsoft.AspNetCore.Mvc;
using Orange_Portfolio_BackEnd.Models;
using Orange_Portfolio_BackEnd.Models.Interfaces;

namespace Orange_Portfolio_BackEnd.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listUsers = _userRepository.GetAll();
            return Ok(listUsers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return user == null ? NotFound("User not found!") : Ok(user);
        }

        [HttpPost]
        public IActionResult Add([FromBody] User user)
        {
            _userRepository.Add(user);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] User user, int id)
        {
            _userRepository.Update(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return Ok();
        }
    }
}
