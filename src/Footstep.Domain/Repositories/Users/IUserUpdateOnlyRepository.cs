using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Users
{
    public interface IUserUpdateOnlyRepository
    {
        Task<User?> GetById(Guid id);
        void Update(User user);
    }
}
