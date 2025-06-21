using Footstep.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Domain.Repositories.Traces
{
    public interface ITracesUpdateOnlyRepository
    {
        Task<Trace?> GetById(Guid id);
        void Update(Trace trace);
    }
}
