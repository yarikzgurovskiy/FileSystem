using FileSystem.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.BLL.Interfaces {
    public interface IAccountService {
        /// <summary>
        /// Register new User in FileSystem
        /// </summary>
        /// <param name="userDto">DTO to register</param>
        /// <returns>Result of registration</returns>
        Task<IdentityResult> RegisterAsync(UserDTO userDto);

        /// <summary>
        /// Authenticate User in FileSystem
        /// </summary>
        /// <param name="userDto">User to authenticate</param>
        /// <param name="isRemember">Remember user in browser</param>
        /// <returns>Result of authentication</returns>
        Task<SignInResult> AuthenticateAsync(UserDTO userDto, bool isRemember);

        /// <summary>
        /// Checks that user with concrete username already registered
        /// </summary>
        /// <param name="userName">username to check</param>
        /// <returns>Bool result</returns>
        Task<bool> IsAlreadyRegisteredAsync(string userName);

        /// <summary>
        /// Log out current User
        /// </summary>
        void LogOutAsync();
    }
}
