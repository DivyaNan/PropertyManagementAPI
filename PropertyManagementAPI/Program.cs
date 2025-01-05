
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PropertyManagementAPI.Data;
using PropertyManagementAPI.Extensions;
using PropertyManagementAPI.Helpers;
using PropertyManagementAPI.Interfaces;
using PropertyManagementAPI.Middleware;
using System.Text;

namespace PropertyManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DataContext>(options=>
            options.UseSqlServer(connString));
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                            secretKey));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    IssuerSigningKey = key
                                };
                            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
         
            app.CustomExceptionHandler(app.Environment);
            app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

    
            app.MapControllers();

            app.Run();
        }
    }
}
