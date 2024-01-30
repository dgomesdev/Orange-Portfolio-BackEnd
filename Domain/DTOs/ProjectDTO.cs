using Orange_Portfolio_BackEnd.Domain.Models;

namespace Orange_Portfolio_BackEnd.Domain.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; }
        public DateOnly UploadDate { get; set; }
        public ICollection<ProjectTagDTO> ProjectsTags { get; set; }
    }
}
