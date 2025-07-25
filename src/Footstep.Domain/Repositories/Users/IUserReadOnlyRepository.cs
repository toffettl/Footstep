using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Users;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<User?> GetByEmail(string email); 

    Task<User?> GetById(Guid id);
}
