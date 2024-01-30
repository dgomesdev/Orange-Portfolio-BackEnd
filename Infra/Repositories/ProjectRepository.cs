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
        public async Task Update(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            _db.Projects.Remove(await GetById(id));
            await _db.SaveChangesAsync();
        }
    }
}
