using FileSystem.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.BLL.Interfaces {
    public interface IUserService {
        Task<IdentityResult> RegisterAsync(UserDTO userDto);
        Task<SignInResult> AuthenticateAsync(UserDTO userDto, bool isRemember);
        Task<bool> IsAlreadyRegisteredAsync(string userName);
        void LogOutAsync();
    }
}
