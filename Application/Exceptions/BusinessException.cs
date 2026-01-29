namespace Application.Exceptions;

public class BusinessException : Exception
{
    public short StatusCode { get; set; } = 400;
    
    public BusinessException() { }
    public BusinessException(string message) : base(message) { }
}