using AutoMapper;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using FileSystem.DAL.Entities;

namespace FileSystem.BLL.Services {
    public class UserService : IUserService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IFolderService folderService;
        private readonly UserManager<User> userManager;
        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFolderService folderService,
            UserManager<User> userManager
        ) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.folderService = folderService;
            this.userManager = userManager;
        }
        public async void DeleteUser(int userId) {
            FolderDTO folder = mapper.Map<FolderDTO>(unitOfWork.FolderRepository.AllFolders
                .FirstOrDefault(f => !f.FolderId.HasValue && f.UserId == userId));
            folderService.DeleteFolder(folder.Id);

            User user = await userManager.FindByIdAsync(userId.ToString());
            var rolesForUser = await userManager.GetRolesAsync(user);
            if (rolesForUser.Count() > 0) {
                foreach (var item in rolesForUser) {
                    await userManager.RemoveFromRoleAsync(user, item);
                }
            }
            await userManager.DeleteAsync(user);
        }

        public IEnumerable<UserDTO> GetUsers() {
            return mapper.Map<IEnumerable<UserDTO>>(userManager.Users);
        }
    }
}
