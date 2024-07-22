using Bigai.Restaurants.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigai.Restaurants.Infrastructure.Data.Configurations;

internal class DishesConfiguration : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.Property(dish => dish.Price)
               .HasPrecision(5, 2);
    }
}