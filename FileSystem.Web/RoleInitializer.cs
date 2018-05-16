using FileSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Web {
    public static class RoleInitializer {
        public static async Task SeedRoles(IServiceProvider serviceProvider, RoleManager<Role> roleManager) { 
            string[] roles = { "admin", "registered" };
            foreach (var role in roles) {
                if (!await roleManager.RoleExistsAsync(role)) {
                    await roleManager.CreateAsync(new Role() { Name = role });
                }
            }
        }
    }
}
