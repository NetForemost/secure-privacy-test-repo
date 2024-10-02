using Backend.Core.Entities;

namespace Backend.API.Responses;

public record CreateUserResponse(
    string UserName,
    string Email
){
    public static CreateUserResponse FromDomain(User user)
    {
        return new CreateUserResponse(
            UserName: user.UserName,
            Email: user.Email
        );
    }
};