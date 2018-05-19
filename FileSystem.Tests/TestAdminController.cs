using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.Web.Controllers;
using FileSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem.Tests {
    [TestFixture]
    public class TestAdminController {
        [Test]
        public void IndexReturnsAViewResultWithAListOfUsers() {
            // Arrange
            var mock = new Mock<IUserService>();
            mock.Setup(repo => repo.GetUsers()).Returns(GetTestUsers());
            var controller = new AdminController(mock.Object);

            // Act
            var result = controller.Users();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;

            Assert.IsAssignableFrom<UsersViewModel>(viewResult.Model);
            var model = viewResult.Model as UsersViewModel;

            Assert.AreEqual(GetTestUsers().Count, model.Users.Count());
        }

        private List<UserDTO> GetTestUsers() {
            var users = new List<UserDTO> {
                new UserDTO{Id = 4, FirstName="Qqwe", LastName="Wwewe", UserName="Kkj"},
                new UserDTO{Id = 3, FirstName="Kkj", LastName="Jkj", UserName="we"},
                new UserDTO{Id = 5, FirstName="Ooie", LastName="Jjdhf", UserName="ert"},
                new UserDTO{Id = 1, FirstName="Ppopo", LastName="Jjdhf", UserName="hfgh"}
            };
            return users;
        }

    }
}
