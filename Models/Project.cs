using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Orange_Portfolio_BackEnd.Models
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

        
        [Required]
        [MaxLength(255)]
        [DataType(DataType.Upload)]
        public string Image { get; set; }

        [DataType(DataType.Date)]
        public DateOnly UploadDate { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [InverseProperty("Projects")]
        public ICollection<Tag> Tags { get; set; }

        public Project(string title, string link, string description, string image, DateOnly uploadDate, int userId, ICollection<Tag> tags)
        {
            Title = title;
            Link = link;
            Description = description;
            Image = image;
            UploadDate = uploadDate;
            UserId = userId;
            Tags = tags;
        }
    }
}
