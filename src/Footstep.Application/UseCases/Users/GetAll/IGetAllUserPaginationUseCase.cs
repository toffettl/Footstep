using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public interface IGetAllUserPaginationUseCase
    {
        Task<PagedResult<ResponsePaginationUserJson>> Execute(int page, int pageSize);
    }
}
