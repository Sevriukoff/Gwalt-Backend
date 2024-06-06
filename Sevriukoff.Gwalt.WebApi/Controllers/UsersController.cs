using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.WebApi.Common.Attributes;
using Sevriukoff.Gwalt.WebApi.QueryParameters;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll([FromQuery] UserQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        var orderBy = queryParameters.OrderBy;
        
        if (queryParameters.WithStats)
        {
            var userModels = await _userService.GetAllWithStaticsAsync();

            var userViewModels = userModels.Select(model =>
            {
                var userViewModel = _mapper.Map<UserWithStatViewModel>(model);

                userViewModel.TotalLikes = _userService.GetTotalMetricsByTracks(model, t => t.LikesCount);
                userViewModel.TotalListens = _userService.GetTotalMetricsByTracks(model, t => t.ListensCount);
                userViewModel.TotalTracks = _userService.GetTracksCount(model);
                userViewModel.TotalGenres = _userService.GetAllGenres(model);

                return userViewModel;
            });
            
            return Ok(userViewModels);
        }
        
        return Ok(await _userService.GetAllAsync(includes, orderBy));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromJwtClaims("sub")] int userId, int id, [FromQuery] UserQueryParameters queryParameters)
    {
        if (userId > 0 && userId == id)
        {
            
        }
        
        var includes = queryParameters.Includes?.Split(';');
        
        var userModel = await _userService.GetByIdAsync(id, includes);
        var userViewModel = _mapper.Map<UserViewModel>(userModel);
        
        return Ok(userViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(UserRegisterViewModel user)
    {
        var id = await _userService.AddAsync(user.Name, user.Email, user.Password);
        
        return Ok(id);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(User user)
    {
        return Ok(await _userService.UpdateAsync(new UserModel()));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _userService.DeleteAsync(id));
    }
}