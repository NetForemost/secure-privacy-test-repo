using Backend.API.Requests;
using Backend.API.Responses;
using Backend.API.Validators;
using Backend.Core.Entities;
using Backend.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[Controller]
[Route("[controller]")]
public class UsersController(IUsersService usersService): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var validator = new CreateUserRequestValidator();
        var validationResult = validator.Validate(request);

        if(!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        User user = request.ToDomain();
        CreateUserResponse response = CreateUserResponse.FromDomain(user);

        await usersService.CreateUserAsync(user);
        return CreatedAtAction(
            actionName: nameof(Create),
            routeValues: new { user.Id },
            value: response
        );  
    }
}