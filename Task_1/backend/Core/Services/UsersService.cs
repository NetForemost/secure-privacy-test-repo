using Backend.Core.Entities;
using Backend.Core.Errors;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Interfaces.Services;

namespace Backend.Core.Services;

public class UsersServices(IUsersRepository usersRepository) : IUsersService
{
    public Task CreateUserAsync(User user)
    {   
        if(!user.HasConsented)
        {
            throw new UnauthorizedException("User has not consented to the terms and conditions");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        return usersRepository.CreateUserAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        User? user = await usersRepository.GetUserByIdAsync(id);

        if(user == null)
        {
            throw new NotFoundException("User not found");
        }

        await usersRepository.DeleteUserAsync(id);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        User? result = await usersRepository.GetUserByEmailAsync(email);

        if(result == null)
        {
            throw new NotFoundException($"User with email {email} not found");
        }

        return result;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        User? result = await usersRepository.GetUserByIdAsync(id);

        if(result == null)
        {
            throw new NotFoundException("User not found");
        }

        return result;
    }

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        return usersRepository.GetUsersAsync();
    }

    public async Task UpdateUserAsync(Guid id, User user)
    {
        User? result = await usersRepository.GetUserByIdAsync(id);

        if(result == null)
        {
            throw new NotFoundException("User not found");
        }

        User anonymousData = new User
        {
            Id = result.Id,
            UserName = "",
            Password = "",
            HasConsented = result.HasConsented
        };

        await usersRepository.UpdateUserAsync(id, anonymousData);
    }
}