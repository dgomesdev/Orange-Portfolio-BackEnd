
using Microsoft.EntityFrameworkCore;
using Orange_Portfolio_BackEnd.Data;
using Orange_Portfolio_BackEnd.Models.Interfaces;
using Orange_Portfolio_BackEnd.Repositories;

namespace Orange_Portfolio_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Dependency injection repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();

            builder.Services.AddControllers();

            // Configuring Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuration for connecting to the database.
            builder.Services.AddDbContext<Context>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrangePortfolioDb"))
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
