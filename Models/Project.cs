using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orange_Portfolio_BackEnd.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Image é obrigatório.")]
        [StringLength(45)]
        public string Title { get; set; }

        [Url]
        [MaxLength(255)]
        public string Link { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        
        [Required]
        [MaxLength(255)]
        [DataType(DataType.Upload)]
        public string image { get; set; }

        [DataType(DataType.Date)]
        public DateOnly UploadDate { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [InverseProperty("Projects")]
        public ICollection<Tag> Tags { get; set; }
    }
}
