using Microsoft.AspNetCore.Identity;

namespace Bigai.Restaurants.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}