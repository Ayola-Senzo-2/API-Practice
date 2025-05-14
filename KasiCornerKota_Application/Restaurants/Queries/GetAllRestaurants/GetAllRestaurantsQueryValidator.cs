

using FluentValidation;
using KasiCornerKota_Application.Restaurants.Dtos;

namespace KasiCornerKota_Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowPageSize = [5, 10, 15, 20, 30];
    private string[] allowedSortBy = [nameof(RestaurantDto.Name),nameof(RestaurantDto.Description),nameof(RestaurantDto.Category)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.PageCount)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0");
        RuleFor(x => x.PageSize)
            .Must(value => allowPageSize.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",",allowPageSize)}]");
        RuleFor(x => x.SortBy)
            .Must(value => string.IsNullOrEmpty(value) || allowedSortBy.Contains(value))
            .WithMessage($"Sort By is optional or must be one of the following values: {string.Join(", ", allowedSortBy)}");

    }
}
