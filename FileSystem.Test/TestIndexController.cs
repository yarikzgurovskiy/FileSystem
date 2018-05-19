using NUnit.Framework;
using FileSystem.BLL.DTO;
using Moq;
using System;

namespace FileSystem.Test {
    [TestFixture]
    public class TestIndexController {
        [Test]
        public void TestMethod1IndexReturnsAViewResultWithAListOfFileSystemElements() {
        }

        private List<FolderDTO> GetTestFolders() {
            var folders = new List<FolderDTO> {

            };
            return folders;
        }
    }
}
