using Core.Common;
using Domain.Entites;

namespace Core.RepositoryContracts
{
    public interface IRestaurantRepository
    {
        public Task<IEnumerable<Restaurant>>GetAllAsync();
        public Task<(List<Restaurant>,int)> GetAllMatchedResultsAsync(string searchPharse,int pageSize,int pageNumber,string? SortBy,SortDirection sortDirection);
        public Task<Restaurant?> GetRestaurantAsync(int id);
        public Task<int> AddRestaurantAsync(Restaurant resturant);
        public Task UpdateRestaurant(Restaurant Obj);
        public Task DeleteRestaurant(Restaurant Obj);

        public Task SaveChanges();
    }
}
