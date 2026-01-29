using Application.Dtos;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(UserCredentialsDto userCredentialsDto);
}