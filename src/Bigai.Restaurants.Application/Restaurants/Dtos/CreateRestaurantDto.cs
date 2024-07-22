using System.ComponentModel.DataAnnotations;

namespace Bigai.Restaurants.Application.Restaurants.Dtos;

public class CreateRestaurantDto
{
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Inser a valida category")]
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    [EmailAddress(ErrorMessage = "Please provide a valid email addres")]
    public string? ContactEmail { get; set; }

    [Phone(ErrorMessage = "Please provide a valid phone number")]
    public string? ContactNumber { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    [RegularExpression(@"^\d\d{2}-\d\d{3}$", ErrorMessage = "Please provide a valid postal code (XX-XXX)")]
    public string? PostalCode { get; set; }
}