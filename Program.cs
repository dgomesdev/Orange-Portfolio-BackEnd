
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

            // 
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuração para conexão com o banco de dados.
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
