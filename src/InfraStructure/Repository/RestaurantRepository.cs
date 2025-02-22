using Core.Common;
using Core.RepositoryContracts;
using Domain.Entites;
using InfraStructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfraStructure.Repository
{
    internal class RestaurantRepository:IRestaurantRepository
    {
        private readonly ApplicationDbContext _Db;
        public RestaurantRepository(ApplicationDbContext db)
        {
            _Db = db;
        }
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
          IEnumerable<Restaurant> resturants= await _Db.Restaurants.Include(r=>r.Dishes).ToListAsync();
            return resturants;
        }
        public async Task<Restaurant?> GetRestaurantAsync(int id)
         {
            var restaurant= await _Db.Restaurants.Include(r=>r.Dishes).FirstOrDefaultAsync(n=>n.ID==id);
            return restaurant;
        }
        public async Task<int> AddRestaurantAsync(Restaurant restaurant)
        {
           await _Db.Restaurants.AddAsync(restaurant);
            _Db.SaveChanges();
            return restaurant.ID;
        }

        public async Task DeleteRestaurant(Restaurant Obj)
        {
            _Db.Remove(Obj);
           await _Db.SaveChangesAsync();
        }

        public async Task UpdateRestaurant(Restaurant Obj)
        {
            _Db.Update(Obj);
            await _Db.SaveChangesAsync();
        }

        public Task SaveChanges() => _Db.SaveChangesAsync();

        public async Task<(List<Restaurant>,int)> GetAllMatchedResultsAsync( string searchPharse,int pageSize,int pageNumber,
            string? SortBy, SortDirection sortDirection)
        {
            
            var searchPharseLower = searchPharse.ToLower();
            var matchedResults =  _Db.Restaurants.Where(n => n.Name.ToLower().Contains(searchPharseLower) || n.Description
              .ToLower().Contains(searchPharseLower));

            if (SortBy != null)
            {
                var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name),r=>r.Name },
                    {nameof(Restaurant.Description),r=>r.Description},
                    {nameof(Restaurant.Category),r=>r.Category}
                };
                var selectedColumn = columnSelector[SortBy];
                matchedResults = sortDirection == SortDirection.Asc ?
                   matchedResults.OrderBy(selectedColumn) : matchedResults.OrderByDescending(selectedColumn);
            }
           

            var TotalCount = await matchedResults.CountAsync();

            var restaurants = await matchedResults.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToListAsync();

            return (restaurants, TotalCount);


        }
    }
}
