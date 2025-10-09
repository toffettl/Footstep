using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Users;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithId(Guid id);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<User?> GetByEmail(string email); 
    Task<User?> GetById(Guid id);
    Task<List<User>> GetAll();
    Task<(List<User> Users, int TotalCount)> GetAllPagination(int page, int pageSize);
    Task<(List<User> Users, int TotalCount)> GetByRanking(int page, int pageSize, DateTime dateTime);
}
