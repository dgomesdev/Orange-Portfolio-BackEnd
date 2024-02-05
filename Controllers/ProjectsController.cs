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
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));

            if (tagNames.Count == 0)
            {
                var projects = await _projectRepository.GetAllExceptUserProjects(userId);
                var projDtos = projects.Select(p => _mapper.Map<ProjectDTO>(p));

                return Ok(projDtos);
            }

            var projectsTags = await _projectRepository.GetByTags(tagNames, userId);
            var projectDtos = projectsTags.Select(p => _mapper.Map<ProjectDTO>(p));

            return Ok(projectDtos);
        }

        [HttpGet("myprojects/tags")]
        public async Task<IActionResult> GetByTagsMyProjects([FromQuery] List<string> tagNames)
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));

            if (tagNames.Count == 0)
            {
                var projects = await _projectRepository.GetMyProjects(userId);
                var projDtos = projects.Select(p => _mapper.Map<ProjectDTO>(p));

                return Ok(projDtos);
            }

            var projectsTags = await _projectRepository.GetByTagsMyProjects(tagNames, userId);
            var projectDtos = projectsTags.Select(p => _mapper.Map<ProjectDTO>(p));

            return Ok(projectDtos);
        }


        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var project = await _projectRepository.GetById(id);
            var projectDTO = _mapper.Map<ProjectDTO>(project);

            return projectDTO == null ? NotFound("Project not found!") : Ok(projectDTO);
        }

        
        [HttpPost]
        public async Task <IActionResult> Add([FromForm] ProjectViewModel model)
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            await _projectRepository.Add(model, userId);

            return StatusCode(201, "Project registered successfully!");
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateProjectViewModel updatedProject, int id)
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            await _projectRepository.Update(id, updatedProject, userId);

            return Ok("Project updated successfully!");
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(_tokenService.GetIdByToken(HttpContext));
            await _projectRepository.Delete(id, userId);

            return Ok("Project removed successfully!");
        }
    }
}