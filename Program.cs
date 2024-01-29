
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Orange_Portfolio_BackEnd.Data;
using Orange_Portfolio_BackEnd.Models.Interfaces;
using Orange_Portfolio_BackEnd.Repositories;
using Orange_Portfolio_BackEnd.Services;
using System.Text;

namespace Orange_Portfolio_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration to use a file with the connection string
            builder.Configuration.AddJsonFile("config.json", optional: false, reloadOnChange: true);

            // Add services to the container.
            // Dependency injection repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();

            // Dependency injection services
            builder.Services.AddScoped<TokenService>();

            builder.Services.AddControllers();

            // Configuring Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            // Configuring to allow using the token in Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            // Jwt configuration
            var key = Encoding.ASCII.GetBytes(builder.Configuration["SecretKey"]);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

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
