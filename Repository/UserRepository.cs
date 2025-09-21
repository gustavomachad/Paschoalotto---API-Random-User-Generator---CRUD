using Microsoft.EntityFrameworkCore;
using RandomUserImporter.Models;

namespace RandomUserImporter.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> AddUsersAsync(List<User> users)
    {
        await dbContext.Users.AddRangeAsync(users);
        var result = await dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<User>> GetUsersAsync(int quantity)
    {
        return await dbContext.Users.Take(quantity).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await dbContext.Users.FindAsync(id);
    }

    public async Task<User> AddUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var exists = await dbContext.Users.AnyAsync(u => u.Id == user.Id);
        if (!exists) return false;

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null) return false;

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
