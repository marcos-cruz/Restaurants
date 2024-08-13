using FluentValidation;

namespace Bigai.Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] _allowedPageSizes = [5, 10, 15, 30];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(property => property.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(property => property.PageSize)
            .Must(value => _allowedPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(", ", _allowedPageSizes)}]");
    }
}