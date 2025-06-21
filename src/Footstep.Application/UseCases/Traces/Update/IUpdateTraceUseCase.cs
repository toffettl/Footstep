using Footstep.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Traces.Update
{
    public interface IUpdateTraceUseCase
    {
        Task Execute(Guid id, RequestTraceJson request);
    }
}
