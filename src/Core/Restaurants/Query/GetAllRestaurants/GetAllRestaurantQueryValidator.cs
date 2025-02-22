using Core.Restaurants.Dtos;
using FluentValidation;

namespace Core.Restaurants.Query.GetAllRestaurants
{
    public class GetAllRestaurantQueryValidator: AbstractValidator<GetAllRestaurantQuery>
    {
        private int[] allowPageSizes = [5, 10, 15, 30];
        private string[] AllowedSortBy = [nameof(RestaurantDTO.Name),nameof(RestaurantDTO.Description),
        nameof(RestaurantDTO.Category)];
        public GetAllRestaurantQueryValidator()
        {
           RuleFor(r=>r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Must(value => allowPageSizes.Contains(value)).
                WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

            RuleFor(r => r.SortBy).Must(value => AllowedSortBy.Contains(value)).
                When(r=>r.SortBy!=null)
                .
                WithMessage($"SortBy is optional , or must be in [{string.Join(",", allowPageSizes)}]");

        }
    }
}
