using Domain.Entites;

namespace Core.RepositoryContracts
{
    public interface IDishRepository
    {
        Task<int> Create(Dish dish);
        Task Delete (IEnumerable<Dish> dishes);
    }
}
