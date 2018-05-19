﻿using FileSystem.DAL.Entities;
using System.Threading.Tasks;

namespace FileSystem.DAL {
    public interface IApplicationUserAccessor {
        int GetUserId();
        Task<User> GetUser();
        bool IsAdmin();
    }
}