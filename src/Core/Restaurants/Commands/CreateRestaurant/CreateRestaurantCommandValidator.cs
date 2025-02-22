using FluentValidation;

namespace Core.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Indian", "Japanese", "American", "Egyptian"];
        public CreateRestaurantCommandValidator()
        {

            RuleFor(o => o.Name).Length(2, 50);

            RuleFor(o => o.Description).Length(2, 100).NotEmpty().WithMessage("Description is Required");

            RuleFor(o => o.Category).Length(2, 30).Must(category => validCategories.Contains(category))
                .NotEmpty().WithMessage("Enter a valid Category");

            RuleFor(o => o.ContactEmail).EmailAddress().WithMessage("Enter a Valid Email Address");

            RuleFor(user => user.ContactNumber)
            .Matches(@"^\+?[1-9]\d{0,2}[-.\s]?\(?\d{1,4}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$")
            .WithMessage("Invalid phone number format.");

            RuleFor(o => o.PostalCode).Matches(@"^\d{2}-\d{3}$").WithMessage("Please enter a valid postal code(XX - XXX)");
        }
    }
}