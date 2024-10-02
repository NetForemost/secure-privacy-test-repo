using Backend.Core.Entities;

namespace Backend.API.Responses;

public record GetUserResponse(
    Guid id,
    string UserName,
    string Email
){
    public static GetUserResponse FromDomain(User user)
    {
        return new GetUserResponse(
            id: user.Id,
            UserName: user.UserName,
            Email: user.Email
        );
    }
};