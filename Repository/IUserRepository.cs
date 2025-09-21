using RandomUserImporter.Models;

namespace RandomUserImporter.Repositories;

public interface IUserRepository
{
    Task<bool> AddUsersAsync(List<User> users);
    Task<List<User>> GetUsersAsync(int quantity);

    Task<User?> GetByIdAsync(Guid id);
    Task<User> AddUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(Guid id);
}
