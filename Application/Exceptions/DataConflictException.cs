namespace Application.Exceptions;

public class DataConflictException : BusinessException
{
    public DataConflictException(string message) : base(message)
    {
        StatusCode = 409;
    }
}