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

        public List<Project> Get()
        {
            return _db.Projects.ToList();
        }
        public Project Get(int id)
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
            _db.Projects.Remove(Get(id));
            _db.SaveChanges();
        }

        public ICollection<Project> GetAll()
        {
            throw new NotImplementedException();
        }

        public Project GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
