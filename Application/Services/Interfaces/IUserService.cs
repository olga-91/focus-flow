using Application.Dtos;

namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterAsync(UserRegisterDto userDto);
    Task<IEnumerable<UserDto>> GetAllAsync();
}