using Orange_Portfolio_BackEnd.Domain.Models;

namespace Orange_Portfolio_BackEnd.Domain.Models.Interfaces
{
    public interface IProjectRepository
    {
        Task<ICollection<Project>> GetAll();
        Task<Project> GetById(int id);
        Task Add(Project project);
        Task Update(Project project);
        Task Delete(int id);
    }
}
