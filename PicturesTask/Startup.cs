using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicturesTask.Infrastructure;
using PicturesTask.Infrastructure.Entities;
using PicturesTask.Middlewares;

namespace PicturesTask
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UsersContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("IdentityDbConnection"));
            })
                .AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<UsersContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            app.UseRouting();

            using (var scope =
                        app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<UsersContext>())
                context.Database.EnsureCreated();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}