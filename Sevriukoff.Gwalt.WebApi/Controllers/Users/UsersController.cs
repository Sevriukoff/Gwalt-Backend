using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
        var pageNumber = queryParameters.PageNumber;
        var pageSize = queryParameters.PageSize;
        
        if (queryParameters.WithStats)
        {
            var userWithStatModels = await _userService.GetAllWithStaticsAsync(orderBy, pageNumber, pageSize);

            var userWithStatViewModels = userWithStatModels.Select(model =>
            {
                var userViewModel = _mapper.Map<UserWithStatViewModel>(model);

                userViewModel.TotalTracks = _userService.GetTracksCount(model);
                userViewModel.TotalGenres = _userService.GetAllGenres(model);

                return userViewModel;
            });
            
            return Ok(userWithStatViewModels);
        }
        
        var userModels = await _userService.GetAllAsync(includes, orderBy, pageNumber, pageSize);
        var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(userModels);
        
        return Ok(userViewModels);
    }
    
    [HttpGet("{userId:int}/followers")]
    public async Task<IActionResult> GetFollowers(int userId, [FromQuery] UserQueryParameters queryParameters)
    {
        var followers = await _userService.GetFollowersAsync(userId);
        
        return Ok(_mapper.Map<IEnumerable<UserFloatViewModel>>(followers));
    }
    
    [HttpGet("{userId:int}/followings")]
    public async Task<IActionResult> GetFollowings(int userId, [FromQuery] UserQueryParameters queryParameters)
    {
        var followings = await _userService.GetFollowingsAsync(userId);
        
        return Ok(_mapper.Map<IEnumerable<UserFloatViewModel>>(followings));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromJwtClaims("sub")] int userId, int id, [FromQuery] UserQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        
        if (userId > 0 && userId == id)
        {
            
        }
        
        var userModel = await _userService.GetByIdAsync(id, includes);
        
        if (queryParameters.WithStats)
        {
            var userViewModel = _mapper.Map<UserWithStatViewModel>(userModel);
            userViewModel.TotalTracks = _userService.GetTracksCount(userModel);
            return Ok(userViewModel);
        }
        else
        {
            var userViewModel = _mapper.Map<UserViewModel>(userModel);
            return Ok(userViewModel);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(UserRegisterViewModel userViewModel)
    {
        var userModel = _mapper.Map<UserModel>(userViewModel);
        var id = await _userService.AddAsync(userModel, userViewModel.Password, userViewModel.IsDefaultImages);
        
        return Ok(id);
    }

    #region Follow
    
    [HttpGet("{followerId:int}/follow/{userId:int}")]
    public async Task<IActionResult> GetFollow(int userId, int followerId)
    {
        var createdAt = await _userService.GetFollowAsync(userId, followerId);

        if (createdAt == DateTime.MinValue)
            return NotFound();
        
        return Ok(new { createdAt });
    }
    
    [Authorize]
    [HttpPost("{followerId:int}/follow/{userId:int}")]
    public async Task<IActionResult> Follow([FromJwtClaims("sub")]int authUserId, int userId, int followerId)
    {
        if (authUserId != followerId)
            return Forbid();
        
        var followId = await _userService.FollowAsync(userId, followerId);
        
        return Ok(followId);
    }
    
    [Authorize]
    [HttpDelete("{followerId:int}/follow/{userId:int}")]
    public async Task<IActionResult> Unfollow([FromJwtClaims("sub")]int authUserId, int userId, int followerId)
    {
        if (authUserId != followerId)
            return Forbid();
        
        var followId = await _userService.UnfollowAsync(userId, followerId);
        
        return Ok(followId);
    }
    
    #endregion
    
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<UserUpdateViewModel> userViewModel)
    {
        var user = await _userService.GetByIdAsync(id);
        
        if (user == null)
            return NotFound();
        
        var userUpdateViewModel = _mapper.Map<UserUpdateViewModel>(user);
        
        userViewModel.ApplyTo(userUpdateViewModel);
        _mapper.Map(userUpdateViewModel, user);
    
        await _userService.UpdateAsync(user);
    
        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(User user)
    {
        return Ok(await _userService.UpdateAsync(new UserModel(user.Id)));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _userService.DeleteAsync(id));
    }
}