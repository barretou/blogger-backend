
using Blogger.Repository;
using Blogger.Repository.Context;
using Blogger.Repository.Interfaces;
using Blogger.Services;
using Blogger.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blogger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<AuthService>();

            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // -------------------------------
            // Add CORS policy
            // -------------------------------
            var corsPolicy = "AllowFrontend";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173") // Vue dev server (HTTP)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // -------------------------------
            // IMPORTANT: Use CORS BEFORE Authorization and Controllers
            // -------------------------------
            app.UseCors(corsPolicy);

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
