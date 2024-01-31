using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orange_Portfolio_BackEnd.Application.Services;
using Orange_Portfolio_BackEnd.Application.ViewModel;
using Orange_Portfolio_BackEnd.Domain.DTOs;
using Orange_Portfolio_BackEnd.Domain.Models.Interfaces;

namespace Orange_Portfolio_BackEnd.Controllers
{
    [Authorize]
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectRepository projectRepository, TokenService tokenService, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        
        [HttpGet()]
        public async Task <IActionResult> GetAllProjectsOtherUsers()
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            var projects = await _projectRepository.GetAllExceptUserProjects(userId);
            var projectDtos = projects.Select(p => _mapper.Map<ProjectDTO>(p));
            return Ok(projectDtos);
        }

        
        [HttpGet("myprojects")]
        public async Task<IActionResult> GetMyProjects()
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            var projects = await _projectRepository.GetMyProjects(userId);
            var projectDtos = projects.Select(p => _mapper.Map<ProjectDTO>(p));
           
            return Ok(projectDtos);
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetByTags([FromQuery] List<string> tagNames)
        {
            var projects = await _projectRepository.GetByTags(tagNames);

            if (projects.Count == 0)
            {
                return NotFound("No projects found with the specified tags.");
            }

            return Ok(projects);
        }


        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var project = await _projectRepository.GetById(id);
            var projectDTO = _mapper.Map<ProjectDTO>(project);

            return projectDTO == null ? NotFound("Project not found!") : Ok(projectDTO);
        }

        
        [HttpPost]
        public async Task <IActionResult> Add([FromBody] ProjectViewModel model)
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            await _projectRepository.Add(model, userId);

            return Ok();
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProjectViewModel updatedProject, int id)
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            await _projectRepository.Update(id, updatedProject, userId);

            return Ok();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            await _projectRepository.Delete(id, userId);

            return Ok();
        }
    }
}