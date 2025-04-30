
using FluentValidation;

namespace KasiCornerKota_Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(Dishes => Dishes.Calories)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Calories must be a non-negative number.");

            RuleFor(Dishes => Dishes.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be a non-negative number");
        }
    }
}
