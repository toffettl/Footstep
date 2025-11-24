using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.UserItem
{
    public interface IUserItemReadOnlyRepository
    {
        Task<bool> HasUserPurchasedAsync(Guid userId, Guid itemId);
        Task<List<Entities.UserItem>> GetByUserIdAsync(Guid userId);
    }
}
