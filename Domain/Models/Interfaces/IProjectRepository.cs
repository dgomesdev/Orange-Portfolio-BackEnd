using Orange_Portfolio_BackEnd.Application.ViewModel;

namespace Orange_Portfolio_BackEnd.Domain.Models.Interfaces
{
    public interface IProjectRepository
    {
        Task<ICollection<Project>> GetAll();
        Task<Project> GetById(int id);
        Task<List<Project>> GetAllExceptUserProjects(int userId);
        Task Add(ProjectViewModel model, int idUser);
        Task Update(Project project);
        Task Delete(int id);
    }
}
