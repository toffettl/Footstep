using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.UserItems
{
    public interface IUserItemWriteOnlyRepository
    {
        Task Add(Entities.UserItem userItem);
    }
}
