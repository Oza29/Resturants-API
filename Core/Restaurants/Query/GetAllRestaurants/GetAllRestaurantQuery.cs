using Core.Common;
using Core.Restaurants.Dtos;
using MediatR;

namespace Core.Restaurants.Query.GetAllRestaurants
{
    public class GetAllRestaurantQuery : IRequest<PagedResult<RestaurantDTO>>
    {
        
        public string? searchPharse {  get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy {  get; set; }
        public SortDirection SortDirection { get; set; }=SortDirection.Desc;
    }
}
