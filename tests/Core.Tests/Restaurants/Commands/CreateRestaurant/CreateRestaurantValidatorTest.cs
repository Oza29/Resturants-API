using Core.Restaurants.Commands.CreateRestaurant;
using FluentValidation.TestHelper;

namespace Core.Tests.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantValidatorTest
    {
        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //Arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Test",
                Description = "This is a description",
                Category = "Italian",
                ContactEmail = "test@example.com",
                PostalCode = "12-345"
            };
            var validator = new CreateRestaurantCommandValidator();

            //Act
            var result = validator.TestValidate(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //Arrange
            var command = new CreateRestaurantCommand
            {
                Name = "f",
                Description = "",
                Category = "ita",
                ContactEmail = "@example.com",
                PostalCode = "12345"
            };
            var validator = new CreateRestaurantCommandValidator();

            //Act
            var result = validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.Description);
        }


        [Theory]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Indian")]
        [InlineData("American")]
        [InlineData("Japanese")]
        [InlineData("Egyptian")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorOnPropertyCategory(string category)
        {
            //Arrange
            var command = new CreateRestaurantCommand
            {
                Category=category
            };
            var validator = new CreateRestaurantCommandValidator();

            //Act
            var result = validator.TestValidate(command);

            //Assert
            result.ShouldNotHaveValidationErrorFor(c=>c.Category);
        }
        [Theory]
        [InlineData("12345")]
        [InlineData("110-20")]
        [InlineData("10 330")]
        [InlineData("12-23 30")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorOnPropertyPostalCode(string PostalCode)
        {
            //Arrange
            var command = new CreateRestaurantCommand
            {
                PostalCode = PostalCode
            };
            var validator = new CreateRestaurantCommandValidator();

            //Act
            var result = validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }

    }
}
