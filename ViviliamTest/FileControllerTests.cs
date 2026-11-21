using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ViviliamTest
{
    [TestClass]
    public class FileControllerTests
    {
        private Mock<IWebHostEnvironment> _mockEnv = null!;
        private FileController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockEnv = new Mock<IWebHostEnvironment>();
            _mockEnv.Setup(env => env.WebRootPath).Returns("wwwroot");
            _controller = new FileController(_mockEnv.Object);
        }

        [TestMethod]
        public void GetImage_json_InvalidFileName_ReturnsBadRequest()
        {
            // Arrange
            var invalidFileName = string.Empty;

            // Act
            var result = _controller.GetImage_json(invalidFileName);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Invalid file name.", badRequestResult.Value);
        }

        [TestMethod]
        public void GetImage_json_FileExists_ReturnsPhysicalFile()
        {
            // Arrange
            var fileName = "validFileName.json";
            var filePath = Path.Combine("wwwroot/assets/voiceover/", fileName);
            Directory.CreateDirectory("wwwroot/assets/voiceover/");
            System.IO.File.WriteAllText(filePath, "{}"); // Create a dummy file

            // Act
            var result = _controller.GetImage_json(fileName);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PhysicalFileResult));
            var physicalFileResult = result as PhysicalFileResult;
            Assert.IsNotNull(physicalFileResult);
            Assert.AreEqual("application/json", physicalFileResult.ContentType);
            Assert.AreEqual(filePath, physicalFileResult.FileName);

            // Clean up
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        [TestMethod]
        public void GetImage_json_FileNotExists_ReturnsNotFound()
        {
            // Arrange
            var fileName = "nonExistentFile.json";
            _mockEnv.Setup(env => env.WebRootPath).Returns("wwwroot");

            // Act
            var result = _controller.GetImage_json(fileName);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("File not found.", notFoundResult.Value);
        }
    }
}