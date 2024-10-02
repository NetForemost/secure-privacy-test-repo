namespace Backend.Core.Errors;

public class UnauthorizedException(string message)
: ServiceException(StatusCodes.Status401Unauthorized, message)
{
    
}