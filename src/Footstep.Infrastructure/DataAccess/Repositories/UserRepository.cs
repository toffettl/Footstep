using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories;
public class UserRepository : 
    IUserReadOnlyRepository, 
    IUserWriteOnlyRepository, 
    IUserUpdateOnlyRepository
{
    private readonly FootstepDbContext _dbContext;

    public UserRepository(FootstepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.Users
            .AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users
            .AnyAsync(user => user.Email!.Equals(email));
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AsSplitQuery()
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .Include(u => u.PointsOfInterest)
            .Include(u => u.Coin)
            .Include(u => u.Preference)
                .ThenInclude(p => p.Image)
            .Include(u => u.Preference)
                .ThenInclude(p => p.Items.Where(i => i.Unlocked))
                    .ThenInclude(i => i.Style)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AsSplitQuery()
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .Include(u => u.PointsOfInterest)
            .Include(u => u.Coin)
            .Include(u => u.Preference)
                .ThenInclude(p => p.Image)
            .Include(u => u.Preference)
                .ThenInclude(p => p.Items.Where(i => i.Unlocked))
                    .ThenInclude(i => i.Style)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public void Update(User user)
    {
        _dbContext.Users
            .Update(user);
    }

    public async Task<List<User>> GetAll()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AsSplitQuery()
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .Include(u => u.PointsOfInterest)
            .Include(u => u.Coin)
            .Include(u => u.Preference)
                .ThenInclude(p => p.Image)
            .Include(u => u.Preference)
                .ThenInclude(p => p.Items.Where(i => i.Unlocked))
                    .ThenInclude(i => i.Style)
            .ToListAsync();
    }

    public async Task<(List<User> Users, int TotalCount)> GetAllPagination(int page, int pageSize)
    {
        var query = _dbContext.Users
            .AsTracking()
            .AsSplitQuery()
            .Include(u => u.Preference)
                .ThenInclude(p => p.Image)
            .Include(u => u.Preference)
                .ThenInclude(p => p.Items.Where(i => i.Unlocked && i.Equipped))
                    .ThenInclude(i => i.Style);

        var totalCount = await query.CountAsync();

        var users = await query
            .OrderByDescending(u => u.UserPointOfInterestRelations.Count)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, totalCount);
    }

    public async Task<bool> ExistActiveUserWithId(Guid id)
    {
        return await _dbContext.Users
            .AnyAsync(user => user.Id!.Equals(id));
    }

    public async Task<(List<User> Users, int TotalCount)> GetByRanking(int page, int pageSize, DateTime dateTime)
    {
        var query = _dbContext.Users
            .AsTracking()
            .AsSplitQuery()
            .Include(u => u.Preference)
                .ThenInclude(p => p.Items.Where(i => i.Unlocked && i.Equipped))
                    .ThenInclude(i => i.Style);

        var totalCount = await query.CountAsync();

        var users = await query
            .OrderByDescending(u => u.PointsOfInterest.Where(poi => poi.CreatedAt > dateTime).Count())
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, totalCount);
    }
}
