using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetAllByPage
{
    public interface IGetAllPointsOfInterestByPageUseCase
    {
        Task<PagedResult<ResponsePointOfInterestJson>> Execute(int page, int pageSize);
    }
}
