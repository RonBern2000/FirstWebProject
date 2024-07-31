using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Repository
{
    public class Repository : IRepository
    {
        private ZooContext _zooContext;
        public Repository(ZooContext zooContext)
        {
            _zooContext = zooContext;
        }
        public void SaveChanges() => _zooContext.SaveChanges();
        public IEnumerable<Animal> GetAnimals() => _zooContext.Animals!;
        public IEnumerable<Category> GetCategories() => _zooContext.Categories!;
        public IEnumerable<Comment> GetComments() => _zooContext.Comments!;
        public List<Category> GetAllData()
        {
            var data = _zooContext.Categories!.Include(categorty => categorty.Animals!).ThenInclude(comment => comment.Comments!).ToList();
            return data;
        }
        public IEnumerable<Animal> Top2Aniamls()
        {
            var animals = _zooContext.Animals!
                .OrderByDescending(a => a.Comments!.Count)
                .Take(2)
                .AsNoTracking()
                .ToList();
            return animals;
        }
        IEnumerable<Animal> IRepository.GetAnimals(string category)
        {
            var animals = _zooContext.Animals!.Where(a => a.Category!.Name == category);
            return animals;
        }
        public Animal GetAnimal(int id) => _zooContext.Animals!.Single(a => a.AnimalId == id);
        public void AddComment(Comment comment) => _zooContext.Comments!.Add(comment);
    }
}
