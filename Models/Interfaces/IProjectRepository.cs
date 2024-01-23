namespace Orange_Portfolio_BackEnd.Models.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<Project> GetAll();
        Project GetById(int id);
        void Add(Project project);
        void Update(Project project);
        void Delete(int id);
    }
}
