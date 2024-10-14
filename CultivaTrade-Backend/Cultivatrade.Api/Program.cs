using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.Logics;
using Cultivatrade.Api.Services;
using FileUpload.Api.Logics;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CultivatradeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<AdminLogic>();
            builder.Services.AddScoped<CategoryLogic>();
            builder.Services.AddScoped<CheckFileType>();
            builder.Services.AddScoped<EmailSender>();
            builder.Services.AddScoped<FeedbackLogic>();
            builder.Services.AddScoped<ProductLogic>();
            builder.Services.AddScoped<ProductFileLogic>();
            builder.Services.AddScoped<UserLogic>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); ;

            app.MapControllers();

            app.Run();
        }
    }
}
