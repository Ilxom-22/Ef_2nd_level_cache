using EasyCaching_Interceptor.Domain;
using EasyCaching_Interceptor.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyCaching_Interceptor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers([FromQuery]FilterPagination paginationOptions)
    {
        return Ok(userService.GetUsers(paginationOptions));
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser(User user)
    {
        return Ok(await userService.UpdateUserAsync(user));
    }
}