# Setup Infrastructure

Represents the layer responsible, for example, for accessing the database and external data.

- [Setup Infrastructure](#setup-infrastructure)
  - [Required Tool](#required-tool)
  - [Entity Framework](#entity-framework)
  - [Adding Restaurant Db Context](#adding-restaurant-db-context)
  - [Generating Migrations](#generating-migrations)
  - [Applying Migrations](#applying-migrations)
  
[Back to Index](../README.md)

## Required Tool

```powershell

dotnet tool install --global dotnet-ef

```

[Back to top](#setup-infrastructure)

## Entity Framework

```powershell

dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0

```
[Back to top](#setup-infrastructure)

## Adding Restaurant Db Context

```c#
namespace Bigai.Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext : DbContext
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=RestaurantsDb; User Id=sa; Password=[SA PASSWORD]; TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
```
[Back to top](#setup-infrastructure)

## Generating Migrations

```powershell

dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0

```

```powershell

dotnet ef migrations add InitialCreate --output-dir Data\Migrations

```

[Back to top](#setup-infrastructure)

## Applying Migrations

```powershell

dotnet ef database update

```

[Back to top](#setup-infrastructure)