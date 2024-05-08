using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync(new IncludingSpecification<User>("Albums", "Albums.Tracks", "Albums.Tracks.Genres"));
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllWithStaticsAsync()
    {
        return await _userRepository.GetAllWithStaticsAsync();
    }

    public async Task<User> AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}