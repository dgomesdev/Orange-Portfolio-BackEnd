﻿using Orange_Portfolio_BackEnd.Models;

namespace Orange_Portfolio_BackEnd.ViewModel
{
    public class ProjectViewModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateOnly UploadDate { get; set; }
        public int UserId { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
