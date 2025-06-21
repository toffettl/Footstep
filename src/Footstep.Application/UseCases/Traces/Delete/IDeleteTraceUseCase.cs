using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Traces.Delete
{
    public interface IDeleteTraceUseCase
    {
        Task Execute(Guid id);
    }
}
