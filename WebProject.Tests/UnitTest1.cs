using Moq;
using WebProject.Controllers;
using WebProject.Services;
using ZooLib.Models;
using ZooLib.Repository;
using Microsoft.Extensions.Logging;

namespace WebProject.Tests
{
    [TestClass]
    public class UnitTest1
    {
        // Moq FrameWork -- Optional
        [TestMethod]
        public void TestGetAnimal()
        {
            var animal = new Animal
            {
                AnimalId = 1,
                Name = "Dragon",
                CategoryId = 1,
                Age = 100,
                Description = "scary"
            };

            var animalsRepository = new Mock<IRepository>();

            animalsRepository.Setup(x => x.GetAnimal(It.IsAny<int>())).ReturnsAsync(animal);

            var getAnimalById = animalsRepository.Object.GetAnimal(1);
            Assert.IsNotNull(getAnimalById);
        }
    }
}
//var moqLogger = new Mock<ILogger<CatalogController>>();
//var moqComment = new Mock<ICommentService>();
//var catalogController = new CatalogController(animalsRepository.Object, moqLogger.Object, moqComment.Object);
