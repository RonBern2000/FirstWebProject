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
    }
}
