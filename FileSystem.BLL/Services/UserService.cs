using AutoMapper;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.BLL.Services {
    public class UserService : IUserService {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> AuthenticateAsync(UserDTO userDto, bool isRemember) {
            return await signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, isRemember, false);
        }

        public async Task<IdentityResult> RegisterAsync(UserDTO userDto) {
            User user = new User() {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.UserName
            };

            var result = await userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded) {
                result = await userManager.AddToRoleAsync(user, userDto.Role);
            }
            return result;
        }

        public async Task<bool> IsAlreadyRegisteredAsync(string userName) {
            var user = await userManager.FindByNameAsync(userName);
            return user != null;
        }

        public async void LogOutAsync() {
            await signInManager.SignOutAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    userManager.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
