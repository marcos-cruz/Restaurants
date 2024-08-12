namespace Bigai.Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string HasAtLeast20 = "HasAtLeast20";
    public const string HasCreatedAtLeast2Restaurants = "HasCreatedAtLeast2Restaurants";
}