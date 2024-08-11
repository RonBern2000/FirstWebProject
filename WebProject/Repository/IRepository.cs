using WebProject.Models;

namespace WebProject.Repository
{
    public interface IRepository
    {
        public void SaveChanges();
        public Task SaveChangesAsync();
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<string>> GetCategoriesNames();
        Task<IEnumerable<Comment>> GetComments();
        Task<IEnumerable<Animal>> GetAnimals();
        Task<IEnumerable<Animal>> GetAnimals(string category);
        Task<Animal> GetAnimal(int id);
        Task<IEnumerable<Category>> GetAllData();
        Task<IEnumerable<Animal>> Top2Aniamls();
        public Task AddComment(Comment comment);
        public Task AddAnimal(Animal animal);
        public Task RemoveAnimal(Animal animal);
        public Task<Category> GetCategoryByName(string category);
        public Task<IEnumerable<Comment>> GetAnimalComments(int id);
    }
}