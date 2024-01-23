namespace Orange_Portfolio_BackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Nation { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
