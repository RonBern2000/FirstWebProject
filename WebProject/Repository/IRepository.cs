using WebProject.Models;

namespace WebProject.Repository
{
    public interface IRepository
    {
        public void SaveChanges();
        IEnumerable<Category> GetCategories();
        IEnumerable<Comment> GetComments();
        IEnumerable<Animal> GetAnimals();
        Animal GetAnimal(int id);
        IEnumerable<Animal> GetAnimals(string category);
        List<Category> GetAllData();
        IEnumerable<Animal> Top2Aniamls();
        public void AddComment(Comment comment);
        public void AddAnimal(Animal animal);
        public void RemoveAnimal(Animal animal);
        public Category GetCategoryByName(string category);
    }
}