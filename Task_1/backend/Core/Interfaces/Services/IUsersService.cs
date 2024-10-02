using Backend.Core.Entities;

namespace Backend.Core.Interfaces.Services;

public interface IUsersService
{
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetUsersAsync();
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(Guid id, User user);
    Task DeleteUserAsync(Guid id);
}