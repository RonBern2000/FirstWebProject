using Moq;
using ZooLib.Models;
using ZooLib.Repository;

namespace WebProject.Tests.FakeRepo
{
    [TestClass]
    public class FakeRepoTests
    {
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

        [TestMethod]
        public async Task TestGetTop2()
        {
            var animals = new List<Animal>()
            {
                new Animal{
                AnimalId = 1,
                Name = "Dragon",
                CategoryId = 1,
                Age = 100,
                Description = "scary",
                Comments = new List<Comment>()
                },
                new Animal{
                AnimalId = 2,
                Name = "Dog",
                CategoryId = 2,
                Age = 12,
                Description = "Cute"
                ,Comments = new List<Comment>() { new Comment {AnimalId =2, CommentId = 2, CommentText = "Test" },
                                                  new Comment {AnimalId =2, CommentId = 3, CommentText = "Test" }}
                },
                new Animal{
                AnimalId = 3,
                Name = "Cat",
                CategoryId = 2,
                Age = 10,
                Description = "selfish",
                Comments = new List<Comment>() { new Comment {AnimalId =3, CommentId = 1, CommentText = "Test" } }
                }
            };
            var animalsRepository = new Mock<IRepository>();

            animalsRepository.Setup(x => x.Top2Animals()).ReturnsAsync(animals.OrderByDescending(a => a.Comments!.Count).Take(2));

            var topAnimals = await animalsRepository.Object.Top2Animals();

            Assert.AreEqual(2, topAnimals.Count());
            Assert.AreEqual("Dog", topAnimals.First().Name);
            Assert.AreEqual("Cat", topAnimals.Last().Name);
        }
        [TestMethod]
        public async Task TestAddAnimal()
        {
            var animal = new Animal
            {
                AnimalId = 1,
                Name = "Dragon",
                CategoryId = 1,
                Age = 100,
                Description = "scary",
                Comments = new List<Comment>()
            };
            var animalsRepository = new Mock<IRepository>();

            animalsRepository.Setup(x => x.AddAnimal(It.IsAny<Animal>())).Returns(Task.CompletedTask);

            await animalsRepository.Object.AddAnimal(animal);

            animalsRepository.Verify(x => x.AddAnimal(It.Is<Animal>(a =>
            a.AnimalId == animal.AnimalId &&
            a.Name == animal.Name &&
            a.CategoryId == animal.CategoryId &&
            a.Age == animal.Age &&
            a.Description == animal.Description)), Times.Once, "AddAnimal should be called exactly once with the correct animal.");
        }
    }
}