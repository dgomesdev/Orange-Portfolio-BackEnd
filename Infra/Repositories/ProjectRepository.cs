using Microsoft.EntityFrameworkCore;
using Orange_Portfolio_BackEnd.Application.ViewModel;
using Orange_Portfolio_BackEnd.Domain.Models;
using Orange_Portfolio_BackEnd.Domain.Models.Interfaces;
using Orange_Portfolio_BackEnd.Infra.Data;
using Orange_Portfolio_BackEnd.Infra.Storage;

namespace Orange_Portfolio_BackEnd.Infra.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly Context _db;
        private readonly IBlob _blob;

        public ProjectRepository(Context db, IBlob blob)
        {
            _db = db;
            _blob = blob;
        }

        public async Task<ICollection<Project>> GetMyProjects(int userId)
        {
            var projects = await _db.Projects
                .Include(up => up.User)
                .Include(p => p.ProjectsTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return projects;
        }

        public async Task<Project> GetById(int id)
        {
            return await _db.Projects
                .Include(up => up.User)
                .Include(p => p.ProjectsTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<List<Project>> GetAllExceptUserProjects(int userId)
        {
            var projectsExceptUser = await _db.Projects
                .Include(up => up.User)
                .Include(p => p.ProjectsTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.UserId != userId)
                .ToListAsync();

            return projectsExceptUser;
        }
        public async Task<ICollection<Project>> GetByTagsMyProjects(List<string> tagNames, int userId)
        {
            return await _db.Projects
                .Include(up => up.User)
                .Include(p => p.ProjectsTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.UserId == userId)
                .Where(p => p.ProjectsTags.Any(pt => tagNames.Contains(pt.Tag.Name)))
                .ToListAsync();
        }

        public async Task<ICollection<Project>> GetByTags(List<string> tagNames, int userId)
        {
            return await _db.Projects
                .Include(up => up.User)
                .Include(p => p.ProjectsTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.UserId != userId)
                .Where(p => p.ProjectsTags.Any(pt => tagNames.Contains(pt.Tag.Name)))
                .ToListAsync();
        }

        public async Task Add(ProjectViewModel viewModel, int idUser)
        {
            var projeto = new Project
            {
                UserId = idUser,
                Title = viewModel.Title,
                Link = viewModel.Link,
                Description = viewModel.Description,
                Image = await _blob.Upload(viewModel.Image),
                UploadDate = DateOnly.FromDateTime(DateTime.Now),
                ProjectsTags = viewModel.Tags.Select(tag =>
                {
                    // Try to find the tag in the database
                    var existingTag = _db.Tags.FirstOrDefault(t => t.Name == tag);

                    // If the tag does not exist, create a new one
                    if (existingTag == null)
                    {
                        existingTag = new Tag { Name = tag };
                        _db.Tags.Add(existingTag);
                    }

                    // Returns a new ProjectTag with the existing or new tag
                    return new ProjectTag { Tag = existingTag };
                }).ToList()
            };

            _db.Projects.Add(projeto);
            await _db.SaveChangesAsync();
        }

        public async Task Update(int idProject, UpdateProjectViewModel updatedProject, int userId)
        {
            var existingProject = await GetById(idProject);

            if (existingProject == null) 
            {
                // Project not found
                throw new InvalidOperationException("Project not found");
            }
            else if(existingProject.UserId == userId)
            {
                existingProject.Title = updatedProject.Title;
                existingProject.Link = updatedProject.Link;
                existingProject.Description = updatedProject.Description;

                if (updatedProject.Image != null)
                {
                    existingProject.Image = await _blob.Upload(updatedProject.Image);
                }

                if (updatedProject.Tags != null)
                {
                    UpdateProjectTags(existingProject, updatedProject.Tags);
                }

                await _db.SaveChangesAsync();
            }
            else
            {
                // Project does not belong to the authenticated user
                throw new InvalidOperationException("Unauthorized user");
            }
        }

        private void UpdateProjectTags(Project existingProject, ICollection<string> updatedTags)
        {
            if (existingProject != null && existingProject.ProjectsTags != null)
            {
                existingProject.ProjectsTags.Clear();

                foreach (var updatedTag in updatedTags)
                {
                    var existingTag = _db.Tags.FirstOrDefault(t => t.Name == updatedTag);

                    if (existingTag == null)
                    {
                        existingTag = new Tag { Name = updatedTag };
                        _db.Tags.Add(existingTag);
                    }

                    existingProject.ProjectsTags.Add(new ProjectTag { Tag = existingTag });
                }
            }
        }

        public async Task Delete(int id, int userId)
        {
            var projectToDelete = await GetById(id);

            if (projectToDelete == null)
            {
                // Project not found
                throw new InvalidOperationException("Project not found");
            }
            else if (projectToDelete.UserId == userId)
            {
                _db.Projects.Remove(projectToDelete);
                await _db.SaveChangesAsync();
            }
            else
            {   // Project does not belong to the authenticated user
                throw new InvalidOperationException("Unauthorized user");
            }
        }

    }
}
