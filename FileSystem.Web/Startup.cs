using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSystem.BLL;
using FileSystem.BLL.Interfaces;
using FileSystem.BLL.Services;
using FileSystem.DAL;
using FileSystem.DAL.Entities;
using FileSystem.DAL.Interfaces;
using FileSystem.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileSystem.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddDbContext<FileSystemDbContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("FileSystem"), b
                => b.MigrationsAssembly("FileSystem.DAL")));

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IFolderRepository, FolderRepository>();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped<IApplicationUserAccessor, HttpUserAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<FileSystemDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            });


            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            
            IServiceScopeFactory scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            
            using (IServiceScope scope = scopeFactory.CreateScope()) {
                RoleManager<Role> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                RoleInitializer.SeedRoles(app.ApplicationServices, roleManager).Wait();
            }
            
            if (env.IsDevelopment()) {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
