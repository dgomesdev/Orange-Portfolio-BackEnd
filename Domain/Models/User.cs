using System.ComponentModel.DataAnnotations;

namespace Orange_Portfolio_BackEnd.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        public string LastName { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }

        [Required]
        [StringLength(90)]
        public string Password { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Upload)]
        public string? Avatar { get; set; }

        [StringLength(45)]
        public string? Nation { get; set; }

        public ICollection<Project> Projects { get; set; }

        public User(string name, string lastName, string email, string password)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            Projects = new List<Project>();
            Nation = "Brasil";
        }
    }
}
