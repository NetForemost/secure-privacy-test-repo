using Backend.Core.Entities;
using Backend.Core.Interfaces.Repositories;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class UsersRepository(IMongoCollection<User> users) : IUsersRepository
{
    public async Task CreateUserAsync(User user)
    {
        await users.InsertOneAsync(user);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        User? result = await users.Find(user => user.Email == email).FirstOrDefaultAsync();
        return result;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        User? result = await users.Find(user => user.Id == id).FirstOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        IEnumerable<User> result = await users.Find(_ => true).ToListAsync();
        return result;
    }
}