using MySqlConnector;
using RestauranteAPI.Data;
using System.Configuration;

namespace RestauranteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<Database>();

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("Mysql");
        }
    }
}
