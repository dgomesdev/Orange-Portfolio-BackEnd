using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Orange_Portfolio_BackEnd.Domain.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Title { get; set; }

        [Url]
        [MaxLength(255)]
        public string Link { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Upload)]
        public string? Image { get; set; }

        [DataType(DataType.Date)]
        public DateOnly UploadDate { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public ICollection<ProjectTag> ProjectsTags { get; set; }
    }
}
