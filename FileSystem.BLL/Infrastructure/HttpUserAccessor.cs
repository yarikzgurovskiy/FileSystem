using FileSystem.DAL;
using FileSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.BLL.Infrastructure {
    public class HttpUserAccessor : IApplicationUserAccessor {
        private readonly IHttpContextAccessor context;

        public HttpUserAccessor(IHttpContextAccessor context) {
            this.context = context;
        }

        public int GetUserId() {
            var id = context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return int.Parse(id);
        }

        public bool IsAdmin() {
            return context.HttpContext.User.IsInRole("admin");
        }
    }
}
