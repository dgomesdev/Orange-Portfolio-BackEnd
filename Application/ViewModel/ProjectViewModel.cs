using Orange_Portfolio_BackEnd.Domain.Models;

namespace Orange_Portfolio_BackEnd.Application.ViewModel
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<TagViewModel>? Tags { get; set; }
    }
}
