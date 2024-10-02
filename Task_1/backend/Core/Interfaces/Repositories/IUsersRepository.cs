using Backend.Core.Entities;

namespace Backend.Core.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetUsersAsync();
    Task CreateUserAsync(User user);
}