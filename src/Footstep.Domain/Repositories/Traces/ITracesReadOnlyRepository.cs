using Footstep.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Domain.Repositories.Traces
{
    public interface ITracesReadOnlyRepository
    {
        Task<List<Trace>> GetAll();
        Task<Trace?> GetById(Guid id);
    }
}
