namespace Orange_Portfolio_BackEnd.Models.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}
