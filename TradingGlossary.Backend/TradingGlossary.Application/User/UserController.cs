using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingGlossary.Application.User.Service.Interfaces;

namespace TradingGlossary.Application.User;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserServiceRunner _userServiceRunner;

    public UserController(IUserService userService, IUserServiceRunner userServiceRunner)
    {
        _userService = userService;
        _userServiceRunner = userServiceRunner;
    }
}