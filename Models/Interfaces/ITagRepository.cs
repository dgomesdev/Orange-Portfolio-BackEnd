namespace Orange_Portfolio_BackEnd.Models.Interfaces
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
