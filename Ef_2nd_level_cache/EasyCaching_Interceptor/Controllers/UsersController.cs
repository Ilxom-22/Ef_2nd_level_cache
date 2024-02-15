using EasyCaching_Interceptor.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyCaching_Interceptor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(userService.GetUsers());
    }
}