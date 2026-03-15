using TradingGlossary.Application.User.Model;
using TradingGlossary.Application.User.Service.Interfaces;
using TradingGlossary.Database.Database;

namespace TradingGlossary.Application.User.Service;

public class UserServiceRunner : IUserServiceRunner
{
    private readonly TradingGlossaryDbContext _dbContext;
    private readonly IUserService _userService;

    public UserServiceRunner(TradingGlossaryDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<UserDto> RunCreateUser(CreateUserDto createUserDto)
    {
        var newUser = _userService.CreateUser(createUserDto);
        await _dbContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<UserDto?> RunUpdateUser(int id, UpdateUserDto updateUserDto)
    {
        var updatedUser = await _userService.UpdateUser(id, updateUserDto);
        await _dbContext.SaveChangesAsync();
        return updatedUser;
    }

    public async Task<bool> RunDeleteUser(int id)
    {
        var result = await _userService.DeleteUser(id);
        await _dbContext.SaveChangesAsync();
        return result;
    }
}