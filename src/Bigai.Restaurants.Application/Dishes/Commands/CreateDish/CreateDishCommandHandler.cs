using AutoMapper;

using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Exceptions;
using Bigai.Restaurants.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IDishesRepository _dishesRepository;
    private readonly ILogger<CreateDishCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateDishCommandHandler(IRestaurantsRepository restaurantsRepository,
                                    IDishesRepository dishesRepository,
                                    ILogger<CreateDishCommandHandler> logger,
                                    IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _dishesRepository = dishesRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new dish {@DishRequest}", request);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dish = _mapper.Map<Dish>(request);

        var dishId = await _dishesRepository.CreateAsync(dish);

        return dishId;
    }
}
