using Backend.Core.Entities;

namespace Backend.API.Responses;

public record GetUserResponse(
    string UserName,
    string Email
){
    public static GetUserResponse FromDomain(User user)
    {
        return new GetUserResponse(
            UserName: user.UserName,
            Email: user.Email
        );
    }
};