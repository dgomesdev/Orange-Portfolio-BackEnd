namespace Orange_Portfolio_BackEnd.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
        public DateOnly UploadDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
