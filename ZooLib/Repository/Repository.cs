﻿using Microsoft.EntityFrameworkCore;
using ZooLib.Data;
using ZooLib.Models;

namespace ZooLib.Repository
{
    public class Repository : IRepository
    {
        private ZooContext _zooContext;
        public Repository(ZooContext zooContext)
        {
            _zooContext = zooContext;
        }

        public async Task<IEnumerable<Animal>> GetAnimals() => await _zooContext.Animals!.ToListAsync();
        public async Task<IEnumerable<Category>> GetCategories() => await _zooContext.Categories!.ToListAsync();
        public async Task<IEnumerable<string>> GetCategoriesNames() => await _zooContext.Categories!.Select(c => c.Name!).ToListAsync();
        public async Task<IEnumerable<Comment>> GetComments() => await _zooContext.Comments!.ToListAsync();
        public async Task<IEnumerable<Category>> GetAllData() => await _zooContext.Categories!.Include(categorty => categorty.Animals!).ThenInclude(comment => comment.Comments!).ToListAsync();
        public async Task<IEnumerable<Animal>> Top2Animals() => await _zooContext.Animals!
                .OrderByDescending(a => a.Comments!.Count)
                .Take(2)
                .AsNoTracking()
                .ToListAsync();
        async Task<IEnumerable<Animal>> IRepository.GetAnimals(string category) => await _zooContext.Animals!.Where(a => a.Category!.Name == category).ToListAsync();
        public async Task<Animal> GetAnimal(int id) => await _zooContext.Animals!.Include(a => a.Category).SingleAsync(a => a.AnimalId == id);
        public async Task AddComment(Comment comment)
        {
            _zooContext.Comments!.Add(comment);
            await _zooContext.SaveChangesAsync();
        }
        public async Task AddAnimal(Animal animal)
        {
            _zooContext.Animals!.Add(animal);
            await _zooContext.SaveChangesAsync();
        }
        public async Task RemoveAnimal(Animal animal)
        {
            _zooContext.Animals!.Remove(animal);
            await _zooContext.SaveChangesAsync();
        }
        public async Task<Category> GetCategoryByName(string category) => await _zooContext.Categories!.SingleAsync(c => c.Name == category);
        public async Task SaveChangesAsync() => await _zooContext.SaveChangesAsync();
        public async Task<IEnumerable<Comment>> GetAnimalComments(int id)
        {
            var animal = await _zooContext.Animals!.FirstAsync(a => a.AnimalId.Equals(id));
            return animal.Comments!;
        }
    }
}
