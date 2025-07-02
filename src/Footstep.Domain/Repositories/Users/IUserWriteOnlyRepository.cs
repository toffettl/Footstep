using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Users;
public interface IUserWriteOnlyRepository
{
    Task Add(User user);
}
