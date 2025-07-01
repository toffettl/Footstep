using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories;
public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
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

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email!.Equals(email));
    }
}
