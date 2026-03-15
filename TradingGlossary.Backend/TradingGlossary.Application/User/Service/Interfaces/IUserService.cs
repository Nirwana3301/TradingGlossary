using TradingGlossary.Application.User.Model;

namespace TradingGlossary.Application.User.Service.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto?> GetUserById(int id);
    Task<UserDto?> GetUserByEMail(string email);
    
    UserDto CreateUser(CreateUserDto createUserDto);
    
    Task<UserDto?> UpdateUser(int id, UpdateUserDto updateUserDto);
    
    Task<bool> DeleteUser(int id);
}