using Microsoft.EntityFrameworkCore;
using Orange_Portfolio_BackEnd.Models;

namespace Orange_Portfolio_BackEnd.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
