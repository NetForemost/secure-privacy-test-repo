using Backend.Core.Entities;
using Backend.Core.Interfaces.Repositories;
using MongoDB.Driver;
using Throw;

namespace Backend.Infrastructure.Repositories;

public class UsersRepository(IMongoCollection<User> users) : IUsersRepository
{
    public async Task CreateUserAsync(User user)
    {
        await users.InsertOneAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var result = await users.DeleteOneAsync(user => user.Id == id);
        result.DeletedCount.Throw().IfNegativeOrZero();
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
        IEnumerable<User> result = await users.Find(user => 
            user.Email != string.Empty 
            || user.FirstName != string.Empty
            || user.LastName != string.Empty
            || user.UserName != string.Empty
            || user.Password != string.Empty
            || user.PhoneNumber != string.Empty
        ).ToListAsync();
        return result;
    }

    public async Task UpdateUserAsync(Guid id, User user)
    {
        var result = await users.ReplaceOneAsync(u => u.Id == id, user);
        result.ModifiedCount.Throw().IfNegativeOrZero();
    }
}