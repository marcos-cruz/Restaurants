
using AutoMapper;

using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;

    public DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
                                          ILogger<DeleteRestaurantCommandHandler> logger,
                                          IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting restaurant {RestaurantId}", request.Id);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
        {
            return false;
        }

        await _restaurantsRepository.DeleteAsync(restaurant);

        return true;
    }
}