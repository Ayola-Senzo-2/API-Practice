using FluentValidation;
using KasiCornerKota_Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasiCornerKota_Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly string[] _validCategories = { "Fast Food", "Casual Dining", "Fine Dining", "Cafe", "Buffet" };
        public CreateRestaurantCommandValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 100);
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");
            RuleFor(x => x.Category)
                /*.Must(_validCategories.Contains)
                .WithMessage($"Category must be one of the following: {string.Join(", ", _validCategories)}")*/
                .Custom((value, context) =>
                {
                    if (string.IsNullOrEmpty(value) || !_validCategories.Contains(value))
                    {
                        context.AddFailure("Category", $"Category must be one of the following: {string.Join(", ", _validCategories)}");
                    }
                });
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\d{3}-\d{3}-\d{4}$")
                .WithMessage("Phone number must be a valid format.");
            RuleFor(x => x.ContactEmail)
                .EmailAddress()
                .WithMessage("Contact email must be a valid email address.");
        }
    }
}
