using Footstep.Communication.Responses.Users;
using Footstep.Communication.Responses;

namespace Footstep.Application.UseCases.Users.GetByRanking
{
    public interface IGetUsersByRankingUseCase
    {
        Task<PagedResult<ResponsePaginationUserJson>> Execute(int page, int pageSize, DateTime dateTime);
    }
}
