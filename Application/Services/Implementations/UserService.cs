using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Exceptions;
using Application.Services.Interfaces;
using Domain.Model;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Services.Implementations;

public class UserService(
    IUserRepository repository) : IUserService
{
    public async Task<UserDto> RegisterAsync(UserRegisterDto userDto)
    {
        var userExists = await repository.UserExists(userDto.Username);

        if (userExists)
        {
            throw new DataConflictException("Username already exists");
        }

        var newUser = new User
        {
            Username = userDto.Username, Password = userDto.Password, Name =  userDto.Name, Email =  userDto.Email
        };
        
        await repository.AddAsync(newUser);
        
        await repository.SaveChangesAsync();

        return new UserDto { Id =  newUser.Id, Username = userDto.Username};
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await repository.GetAllAsync();
        
        return users.Select(x => new  UserDto
        {
            Id =  x.Id,
            Username = x.Username,
            Name = x.Name
        });
    }
}