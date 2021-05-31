using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using Microsoft.AspNetCore.Identity;
// 

namespace ToDoList
{
  public class Startup
  {
    public Startup(IWebHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      services.AddEntityFrameworkMySql()
        .AddDbContext<ToDoListContext>(options => options
        .UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));
      // Here we've added a form of Entity that understands MySQL as a service.

      // We've also configured that service to use a particular database context with the AddDbContext() method, which will be a representation of our database.

      // We further configure Entity Framework to use our default connection by passing it to the UseMySQL() method.

      services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<ToDoListContext>()
      .AddDefaultTokenProviders();
      //We also tell Identity what we want to use as a model for our user with the line services.AddIdentity<ApplicationUser, IdentityRole>().
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseAuthentication();
      app.UseRouting();
      app.UseAuthentication();
      //If app.UseAuthentication(), app.UseRouting(), or any other methods are called in the wrong order, you may run into unhandled exceptions or issues logging in.

      app.UseEndpoints(routes =>
      {
        routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });

      app.UseStaticFiles();

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }
  }
}