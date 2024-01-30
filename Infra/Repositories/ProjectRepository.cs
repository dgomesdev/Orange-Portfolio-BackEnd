using Microsoft.EntityFrameworkCore;
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

        public async Task Add(Project project)
        {
            await _db.Projects.AddAsync(project);
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
