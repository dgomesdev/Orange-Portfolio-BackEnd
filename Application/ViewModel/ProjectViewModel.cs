namespace Orange_Portfolio_BackEnd.Application.ViewModel
{
    public class ProjectViewModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}
