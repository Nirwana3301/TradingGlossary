using TradingGlossary.Application.User.Model;

namespace TradingGlossary.Application.User.Service.Interfaces;

public interface IUserServiceRunner
{
    Task<UserDto> RunCreateUser(CreateUserDto createUserDto);
    
    Task<UserDto?> RunUpdateUser(int id, UpdateUserDto updateUserDto);
    
    Task<bool> RunDeleteUser(int id);
}