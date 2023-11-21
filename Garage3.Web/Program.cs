using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Garage3.Persistence.Data;
using Garage3.Core.Entities;
namespace Garage3.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<GarageContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GarageContext") ?? throw new InvalidOperationException("Connection string 'Exercise_12_Garage_2_0___part_1_Group1Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            new Member { Personnummer = 198702154221, FirstName = "Kalle", LastName = "Karlsson", Membership = Membership.Free };

            using (var scope = app.Services.CreateScope())
            {
                var services= scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<GarageContext>();
                    await new DbInitializer().Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB");
                }
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/ParkVehicles/Error");
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