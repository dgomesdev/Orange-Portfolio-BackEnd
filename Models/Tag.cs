using System.ComponentModel.DataAnnotations;

namespace Orange_Portfolio_BackEnd.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
