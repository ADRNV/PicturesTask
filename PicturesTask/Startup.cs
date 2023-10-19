﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PicturesTask.Core.Models;
using PicturesTask.Core.Repositories;
using PicturesTask.Infrastructure;
using PicturesTask.Infrastructure.Entities;
using PicturesTask.Infrastructure.Entities.MappingConfigurations;
using PicturesTask.Infrastructure.Repositories;
using PicturesTask.Middlewares;
using System.Reflection;

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
                .AddDefaultIdentity<EntityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<UsersContext>();

            services.AddScoped<UserManager<EntityUser>>();

            services.AddScoped<SignInManager<EntityUser>>();

            services.AddSingleton<IPasswordHasher<EntityUser>, PasswordHasher<EntityUser>>();

            services.AddScoped<InvitionsRepository>();

            services.AddScoped<IRepository<CoreFriend>, FriendsRepository>();

            services.AddScoped<IImagesRepository,  ImagesRepository>();

            services.AddAutoMapper(c =>
            {
                c.AddProfile<UserMappingConfiguration>();
                c.AddProfile<FriendMappingConfiguration>();
                c.AddProfile<InvationConfiguration>();
                c.AddProfile<ImageMappingConfiguration>();
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSwaggerGen(c =>
            {
                c.SupportNonNullableReferenceTypes();

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PicturesSharing API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            app.UseRouting();

            using (var scope =
                        app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<UsersContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
               
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