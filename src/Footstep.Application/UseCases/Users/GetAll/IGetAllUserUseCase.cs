using Footstep.Communication.Responses.Users;
using Footstep.Domain.Entities;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public interface IGetAllUserUseCase
    {
        Task<List<ResponseUserJson>> Execute();
    }
}
