using Bigai.Restaurants.Application.Restaurants.Dtos;

using FluentValidation;

namespace Bigai.Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] _allowedPageSizes = [5, 10, 15, 30];
    private readonly string[] _allowedSortColumnNames =
    [
        nameof(RestaurantDto.Name),
        nameof(RestaurantDto.Category),
        nameof(RestaurantDto.Description)
    ];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(property => property.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(property => property.PageSize)
            .Must(value => _allowedPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(", ", _allowedPageSizes)}]");

        RuleFor(property => property.SortBy)
            .Must(value => _allowedSortColumnNames.Contains(value))
            .When(property => property.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(", ", _allowedSortColumnNames)}]");
    }
}