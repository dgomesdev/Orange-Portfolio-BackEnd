using Orange_Portfolio_BackEnd.Application.ViewModel;

namespace Orange_Portfolio_BackEnd.Domain.Models.Interfaces
{
    public interface IProjectRepository
    {
        Task<ICollection<Project>> GetMyProjects(int userId);
        Task<Project> GetById(int id);
        Task<List<Project>> GetAllExceptUserProjects(int userId);
        Task<ICollection<Project>> GetByTags(List<string> tagNames, int userId);
        Task<ICollection<Project>> GetByTagsMyProjects(List<string> tagNames, int userId);
        Task Add(ProjectViewModel model, int idUser);
        Task Update(int idProject, UpdateProjectViewModel updatedProject, int userId);
        Task Delete(int id, int userId);
    }
}
