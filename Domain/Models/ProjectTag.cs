using System.ComponentModel.DataAnnotations.Schema;

namespace Orange_Portfolio_BackEnd.Domain.Models
{
    public class ProjectTag
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

}
