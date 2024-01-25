using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orange_Portfolio_BackEnd.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [InverseProperty("Tags")]
        public ICollection<Project> Projects { get; set; }
    }
}
