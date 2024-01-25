using Orange_Portfolio_BackEnd.Data;
using Orange_Portfolio_BackEnd.Models;
using Orange_Portfolio_BackEnd.Models.Interfaces;

namespace Orange_Portfolio_BackEnd.Repositories
{
    public class ProjectRepository : IProjectRepository 
    {
        private readonly Context _db;

        public ProjectRepository(Context db)
        {
            _db = db;
        }

        public ICollection<Project> GetAll()
        {
            return _db.Projects.ToList();
        }

        public Project GetById(int id)
        {
            return _db.Projects.Find(id)!;
        }

        public void Add(Project project)
        {
            _db.Projects.Add(project);
            _db.SaveChanges();
        }
        public void Update(Project project)
        {
            _db.Projects.Update(project);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db.Projects.Remove(GetById(id));
            _db.SaveChanges();
        }
    }
}
