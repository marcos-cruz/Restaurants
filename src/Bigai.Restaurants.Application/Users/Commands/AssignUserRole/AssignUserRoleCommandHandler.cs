using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, bool>
{
    private readonly ILogger<AssignUserRoleCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserContext _userContext;

    public AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
                                        UserManager<User> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        IUserContext userContext)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _userContext = userContext;
    }

    public async Task<bool> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Assigning user role: {@Request}", request);

        var existingUser = await _userManager.FindByEmailAsync(request.UserEmail) ?? throw new NotFoundException(nameof(User), request.UserEmail);

        var existingRole = await _roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await _userManager.AddToRoleAsync(existingUser, existingRole.Name!);

        return true;
    }
}