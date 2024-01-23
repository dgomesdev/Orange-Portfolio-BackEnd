namespace Orange_Portfolio_BackEnd.Models.Interfaces
{
    public interface ITagRepository
    {
        ICollection<Tag> GetAll();
        Tag GetById(int id);
        void Add(Tag tag);
        void Update(Tag tag);
        void Delete(int id);
    }
}
