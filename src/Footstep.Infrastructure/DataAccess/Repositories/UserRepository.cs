using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories;
public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly FootstepDbContext _dbContext;

    public UserRepository(FootstepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email!.Equals(email));
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email!.Equals(email));
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public async Task<List<User>> GetAll()
    {
        return await _dbContext.Users
            .Include(u => u.Preference)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();
    }
}
