using ZooLib.Models;

namespace ZooLib.Repository
{
    public interface IRepository
    {
        public Task SaveChangesAsync();
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<string>> GetCategoriesNames();
        Task<IEnumerable<Comment>> GetComments();
        Task<IEnumerable<Animal>> GetAnimals();
        Task<IEnumerable<Animal>> GetAnimals(string category);
        Task<Animal> GetAnimal(int id);
        Task<IEnumerable<Category>> GetAllData();
        Task<IEnumerable<Animal>> Top2Animals();
        public Task AddComment(Comment comment);
        public Task AddAnimal(Animal animal);
        public Task RemoveAnimal(Animal animal);
        public Task<Category> GetCategoryByName(string category);
        public Task<IEnumerable<Comment>> GetAnimalComments(int id);
    }
}