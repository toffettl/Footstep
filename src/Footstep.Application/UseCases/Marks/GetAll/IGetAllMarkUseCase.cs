using Footstep.Communication.Responses.Marks;
using Footstep.Domain.Entities;

namespace Footstep.Application.UseCases.Marks.Get
{
    public interface IGetAllMarkUseCase
    {
        Task<ResponseMarksJson> Execute();
    }
}
