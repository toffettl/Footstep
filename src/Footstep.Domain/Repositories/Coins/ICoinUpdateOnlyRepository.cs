using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Coins
{
    public interface ICoinUpdateOnlyRepository
    {
        void Update(Coin coin);
    }
}
