using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Orange_Portfolio_BackEnd.Domain.Models
{
    public class Tag
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<ProjectTag> ProjectsTags { get; set; }
    }
}
