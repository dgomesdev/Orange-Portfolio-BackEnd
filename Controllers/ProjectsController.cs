using Microsoft.AspNetCore.Mvc;
using Orange_Portfolio_BackEnd.Models;
using Orange_Portfolio_BackEnd.Models.Interfaces;

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
        public IActionResult GetAll()
        {
            var listProjects = _projectRepository.GetAll();
            return Ok(listProjects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectRepository.GetById(id);
            return project == null ? NotFound("Project not found!") : Ok(project);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Project project)
        {
            _projectRepository.Add(project);
            return Ok(project);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Project project, int id)
        {
            _projectRepository.Update(project);
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectRepository.Delete(id);
            return Ok();
        }
    }
}