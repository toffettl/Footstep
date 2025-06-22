using Footstep.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Traces.GetAll
{
    public interface IGetAllTraceUseCase
    {
        Task<ResponseTracesJson> Execute();
    }
}
