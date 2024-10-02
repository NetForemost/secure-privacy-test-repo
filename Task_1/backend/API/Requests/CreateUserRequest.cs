using Backend.Core.Entities;

namespace Backend.API.Requests;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string PhoneNumber,
    string Password
){
    public User ToDomain()
    {
        return new User
        {
            FirstName = FirstName,
            LastName = LastName,
            UserName = UserName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Password = Password
        };
    }
}