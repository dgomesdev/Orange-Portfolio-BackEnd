using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orange_Portfolio_BackEnd.Application.Services;
using Orange_Portfolio_BackEnd.Application.ViewModel;
using Orange_Portfolio_BackEnd.Domain.Models;
using Orange_Portfolio_BackEnd.Domain.Models.Interfaces;

namespace Orange_Portfolio_BackEnd.Controllers
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly TokenService _tokenService;

        public ProjectsController(IProjectRepository projectRepository, TokenService tokenService)
        {
            _projectRepository = projectRepository;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            var listProjects = await _projectRepository.GetAllExceptUserProjects(userId);

            return Ok(listProjects);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var project = await _projectRepository.GetById(id);
            return project == null ? NotFound("Project not found!") : Ok(project);
        }

        [Authorize]
        [HttpPost]
        public async Task <IActionResult> Add([FromBody] ProjectViewModel model)
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            await _projectRepository.Add(model, userId);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Project project, int id)
        {
            await _projectRepository.Update(project);
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectRepository.Delete(id);
            return Ok();
        }
    }
}