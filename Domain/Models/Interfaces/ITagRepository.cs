using Orange_Portfolio_BackEnd.Domain.Models;

namespace Orange_Portfolio_BackEnd.Domain.Models.Interfaces
{
    public interface ITagRepository
    {
        Task<ICollection<Tag>> GetAll();
        Task<Tag> GetById(int id);
        Task Add(Tag tag);
        Task Update(Tag tag);
        Task Delete(int id);
    }
}
