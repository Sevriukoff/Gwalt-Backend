using Microsoft.AspNetCore.Mvc;

namespace Sevriukoff.Gwalt.WebApi.Controllers.Users;

[ApiController]
[Route("/api/v1/users/{userId}/followers")]
public class UserFollowersController : ControllerBase
{
    public UserFollowersController()
    {
        
    }
}