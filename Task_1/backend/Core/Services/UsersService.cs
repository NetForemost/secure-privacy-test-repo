using Backend.Core.Entities;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Interfaces.Services;

namespace Backend.Core.Services;

public class UsersServices(IUsersRepository usersRepository) : IUsersService
{
    public Task CreateUserAsync(User user)
    {   
        return usersRepository.CreateUserAsync(user);
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        return usersRepository.GetUserByEmailAsync(email);
    }

    public Task<User> GetUserByIdAsync(Guid id)
    {
        return usersRepository.GetUserByIdAsync(id);
    }

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        return usersRepository.GetUsersAsync();
    }
}