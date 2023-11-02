using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Exercise_12_Garage_2._0___part_1_Group1.Data;
namespace Exercise_12_Garage_2._0___part_1_Group1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Exercise_12_Garage_2_0___part_1_Group1Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Exercise_12_Garage_2_0___part_1_Group1Context") ?? throw new InvalidOperationException("Connection string 'Exercise_12_Garage_2_0___part_1_Group1Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=ParkVehicles}/{action=Index}/{id?}");

            app.Run();
        }
    }
}