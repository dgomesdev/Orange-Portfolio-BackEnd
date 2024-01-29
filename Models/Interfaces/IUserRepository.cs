namespace Orange_Portfolio_BackEnd.Models.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>>GetAll();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task Add(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
