using Domain.Entites;
using FluentValidation;

namespace Core.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantValidation:AbstractValidator<UpdateRestaurantCommand>
    {
       
        public UpdateRestaurantValidation()
        {

            RuleFor(o => o.Name).Length(2, 50);

            RuleFor(o => o.Description).Length(2, 100).NotEmpty().WithMessage("Description is Required");
        }
    }
}
