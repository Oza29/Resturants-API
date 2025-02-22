using Core.Restaurants.Dtos;
using Domain.Entites;

namespace Core.Common
{
    public class PagedResult<T>
    {
        public PagedResult(int pageSize,int pageNumber,int totalCount, IEnumerable<T>items )
        {
            Items = items;
            TotalItemsCount = totalCount;
            TotalPages =(int)Math.Ceiling( totalCount /(double) pageSize);
            ItemsFrom=pageSize*(pageNumber-1)+1;
            ItemsTo = ItemsFrom + pageSize-1;
        }
        public IEnumerable<T> Items {  get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsCount {  get; set; }
        public int ItemsFrom {  get; set; }
        public int ItemsTo { get; set; }
    }
}
