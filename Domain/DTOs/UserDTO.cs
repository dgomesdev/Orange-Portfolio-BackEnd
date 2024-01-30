using Orange_Portfolio_BackEnd.Domain.Models;

namespace Orange_Portfolio_BackEnd.Domain.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Avatar { get; set; }
        public string? Nation { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
