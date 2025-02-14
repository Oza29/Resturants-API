using Core.RepositoryContracts;
using Domain.Entites;
using InfraStructure.AppDbContext;
using InfraStructure.Migrations;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository
{
    internal class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext _db;
        public DishRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<int> Create(Dish dish)
        {
           await _db.AddAsync(dish);
           await _db.SaveChangesAsync();
            return dish.ID;

        }

        public async Task Delete(IEnumerable<Dish> dishes)
        {
            _db.Dishes.RemoveRange(dishes);
            await _db.SaveChangesAsync();
        }
    }
}
