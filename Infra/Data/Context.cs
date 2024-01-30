using Microsoft.EntityFrameworkCore;
using Orange_Portfolio_BackEnd.Domain.Models;

namespace Orange_Portfolio_BackEnd.Infra.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da chave primária composta para a classe de junção ProjetoTag
            modelBuilder.Entity<ProjectTag>()
                .HasKey(pt => new { pt.ProjectId, pt.TagId });

            // Configuração das relações Many-to-Many
            modelBuilder.Entity<ProjectTag>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectsTags)
                .HasForeignKey(pt => pt.ProjectId);

            modelBuilder.Entity<ProjectTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProjectsTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
