using WebProject.Models;

namespace WebProject.Repository
{
    public interface IRepository
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Comment> GetComments();
        IEnumerable<Animal> GetAnimals();
        List<Category> GetAllData();
    }
}
