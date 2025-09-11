using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetAllByPage
{
    public interface IGetAllPointsOfInterestByPageUseCase
    {
        Task<PagedResult<ResponsePaginationPointOfInterestJson>> Execute(int page, int pageSize);
    }
}
