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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<User> users = await usersService.GetUsersAsync();
        IEnumerable<GetUserResponse> response = users.Select(GetUserResponse.FromDomain);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        User user = await usersService.GetUserByIdAsync(id);
        GetUserResponse response = GetUserResponse.FromDomain(user);
        return Ok(response);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail([FromRoute] string email)
    {
        User user = await usersService.GetUserByEmailAsync(email);
        GetUserResponse response = GetUserResponse.FromDomain(user);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await usersService.DeleteUserAsync(id);
        return NoContent();
    }
}