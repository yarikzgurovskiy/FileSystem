using FileSystem.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.BLL.Interfaces {
    public interface IUserService {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>IEnumerable of all users</returns>
        IEnumerable<UserDTO> GetUsers();

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Result of operation</returns>
        Task<IdentityResult> DeleteUserAsync(int userId);

    }
}
