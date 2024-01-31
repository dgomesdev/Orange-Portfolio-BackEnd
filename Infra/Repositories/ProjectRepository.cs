using Microsoft.EntityFrameworkCore;
using Orange_Portfolio_BackEnd.Application.ViewModel;
using Orange_Portfolio_BackEnd.Domain.Models;
using Orange_Portfolio_BackEnd.Domain.Models.Interfaces;
using Orange_Portfolio_BackEnd.Infra.Data;

namespace Orange_Portfolio_BackEnd.Infra.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly Context _db;

        public ProjectRepository(Context db)
        {
            _db = db;
        }

        public async Task<ICollection<Project>> GetAll()
        {
            return await _db.Projects.ToListAsync();
        }

        public async Task<Project> GetById(int id)
        {
            return await _db.Projects.FindAsync(id)!;
        }

        public async Task<List<Project>> GetAllExceptUserProjects(int userId)
        {
            var projectsExceptUser = await _db.Projects
                .Where(p => p.UserId != userId)
                .ToListAsync();

            return projectsExceptUser;
        }

        public async Task Add(ProjectViewModel viewModel, int idUser)
        {
            var projeto = new Project
            {
                UserId = idUser,
                Title = viewModel.Title,
                Link = viewModel.Link,
                Description = viewModel.Description,
                Image = viewModel.Image,
                UploadDate = DateOnly.FromDateTime(DateTime.Now),
                ProjectsTags = viewModel.Tags.Select(tag =>
                {
                    // Tenta encontrar a tag no banco de dados
                    var existingTag = _db.Tags.FirstOrDefault(t => t.Name == tag.Name);

                    // Se a tag não existir, cria uma nova
                    if (existingTag == null)
                    {
                        existingTag = new Tag { Name = tag.Name };
                        _db.Tags.Add(existingTag);
                    }

                    // Retorna um novo ProjectTag com a tag existente ou nova
                    return new ProjectTag { Tag = existingTag };
                }).ToList()
            };

            _db.Projects.Add(projeto);
            await _db.SaveChangesAsync();
        }
        public async Task Update(ProjectViewModel updatedProject, int userId)
        {
            var existingProject = await GetById(updatedProject.Id);

            if (existingProject != null && existingProject.UserId == userId)
            {
                existingProject.Title = updatedProject.Title;
                existingProject.Link = updatedProject.Link;
                existingProject.Description = updatedProject.Description;
                existingProject.Image = updatedProject.Image;

                if (updatedProject.Tags != null)
                {
                    UpdateProjectTags(existingProject, updatedProject.Tags);
                }

                await _db.SaveChangesAsync();
            }
            else
            {   // Projeto não encontrado ou não pertence ao usuário autenticado
                throw new InvalidOperationException("Project not found or does not belong to the authenticated user.");
            }
        }

        private void UpdateProjectTags(Project existingProject, ICollection<TagViewModel> updatedTags)
        {
            if (existingProject != null && existingProject.ProjectsTags != null)
            {
                existingProject.ProjectsTags.Clear();

                foreach (var updatedTag in updatedTags)
                {
                    var existingTag = _db.Tags.FirstOrDefault(t => t.Name == updatedTag.Name);

                    if (existingTag == null)
                    {
                        existingTag = new Tag { Name = updatedTag.Name };
                        _db.Tags.Add(existingTag);
                    }

                    existingProject.ProjectsTags.Add(new ProjectTag { Tag = existingTag });
                }
            }
        }

        public async Task Delete(int id, int userId)
        {
            var projectToDelete = await GetById(id);

            if (projectToDelete != null && projectToDelete.UserId == userId)
            {
                _db.Projects.Remove(projectToDelete);
                await _db.SaveChangesAsync();
            }
            else
            {   // Projeto não encontrado ou não pertence ao usuário autenticado
                throw new InvalidOperationException("Project not found or does not belong to the authenticated user.");
            }
        }
    }
}
