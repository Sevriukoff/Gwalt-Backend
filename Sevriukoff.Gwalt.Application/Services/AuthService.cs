using System.Security.Authentication;
using AutoMapper;
using Sevriukoff.Gwalt.Application.Exceptions;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtHelper _jwtHelper;
    private readonly PasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository,
        JwtHelper jwtHelper,
        PasswordHasher passwordHasher,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtHelper = jwtHelper;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }
    
    public async Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password)
    {
        var userEntity = await _userRepository.GetByEmailAsync(email);
        
        var isVerified = _passwordHasher.VerifyPassword(password, userEntity.PasswordSalt, userEntity.PasswordHash);

        if (!isVerified)
            throw new AuthException("Invalid email or password");
        
        var userModel = _mapper.Map<UserModel>(userEntity);

        var (accessToken, refreshToken) = _jwtHelper.GenerateTokens(userModel);
        
        return (accessToken, refreshToken);
    }
}