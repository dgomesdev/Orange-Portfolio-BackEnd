using Microsoft.AspNetCore.Mvc;
using Orange_Portfolio_BackEnd.Models;
using Orange_Portfolio_BackEnd.Models.Interfaces;
using Orange_Portfolio_BackEnd.ViewModel;

namespace Orange_Portfolio_BackEnd.Controllers
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var listProjects = await _projectRepository.GetAll();
            return Ok(listProjects);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var project = await _projectRepository.GetById(id);
            return project == null ? NotFound("Project not found!") : Ok(project);
        }

        [HttpPost]
        public async Task <IActionResult> Add([FromBody] ProjectViewModel model)
        {
            var newProject = new Project
                (
                model.Title,
                model.Link,
                model.Description,
                model.Image,
                model.UploadDate,
                model.UserId,
                model.Tags
            );
            await _projectRepository.Add(newProject);
            return Ok(newProject);
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