using System.Collections;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllWithStaticsAsync();
    
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<User> DeleteAsync(int id);
}