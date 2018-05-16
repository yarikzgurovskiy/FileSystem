using FileSystem.DAL;
using FileSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.BLL {
    public class HttpUserAccessor : IApplicationUserAccessor {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor context;

        public HttpUserAccessor(UserManager<User> userManager, IHttpContextAccessor context) {
            this.userManager = userManager;
            this.context = context;
        }

        public Task<User> GetUser() {
            return userManager.GetUserAsync(context.HttpContext.User);
        }

        public int GetUserId() {
            var id = context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return int.Parse(id);
        }
    }
}
