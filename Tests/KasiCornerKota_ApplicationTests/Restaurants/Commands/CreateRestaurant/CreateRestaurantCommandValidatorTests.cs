using FluentValidation.TestHelper;
using Xunit;


namespace KasiCornerKota_Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "King Kota",
                Description = "Nyama yase Nhlanzeni hostel and Kota and Carwash",
                Category = "Fast Food",
                PhoneNumber = "067-797-7528",
                ContactEmail = "test@test.com",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Ki",
                Description = "Nyama yase Nhlanzeni hostel and Kota and Carwash",
                Category = "Fast ",
                PhoneNumber = "0677977528",
                ContactEmail = "test.com",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveValidationErrorFor(c =>c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        }
        [Theory]
        [InlineData("Fast Food")]
        [InlineData("Casual Dining")]
        [InlineData("Fine Dining")]
        [InlineData("Cafe")]
        [InlineData("Buffet")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            //arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand() { Category = category };

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

    }
}