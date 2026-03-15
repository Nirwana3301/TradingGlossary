using MarketingLvs.Application.Utils;
using Microsoft.EntityFrameworkCore;
using TradingGlossary.Application.Middlewares;
using TradingGlossary.Application.User.Model;
using TradingGlossary.Application.User.Service.Interfaces;
using TradingGlossary.Database.Database;

namespace TradingGlossary.Application.User.Service;

public class UserService : IUserService
{
    private readonly TradingGlossaryDbContext _dbContext;
    private readonly SessionInfo _sessionInfo;
    private readonly IDateTimeService _dateTimeService;

    public UserService(TradingGlossaryDbContext dbContext, SessionInfo sessionInfo, IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _sessionInfo = sessionInfo;
        _dateTimeService = dateTimeService;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _dbContext.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName
            }).ToListAsync(_sessionInfo.CancellationToken);

        return users;
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName
            }).FirstOrDefaultAsync(_sessionInfo.CancellationToken);

        return user;
    }

    public async Task<UserDto?> GetUserByEMail(string email)
    {
        var user = await _dbContext.Users
            .Where(u => u.PrimaryEmail == email)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName
            }).FirstOrDefaultAsync(_sessionInfo.CancellationToken);

        return user;
    }

    public UserDto CreateUser(CreateUserDto createUserDto)
    {
        var newUser = new Database.Model.User()
        {
            Username = createUserDto.Username,
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            PrimaryEmail = createUserDto.PrimaryEmail,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = _sessionInfo.ClerkUser
        };

        _dbContext.Users.Add(newUser);
        
        var userDto = new UserDto
        {
            Id = newUser.Id,
            Username = newUser.Username,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName
        };
        
        return userDto;
    }

    public async Task<UserDto?> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync(_sessionInfo.CancellationToken);
        
        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.FirstName,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
        
        return userDto;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var userToDelete = await _dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        
        if (userToDelete == null)
        {
            return false;
        }
        
        _dbContext.Users.Remove(userToDelete);
        
        return true;
    }
}