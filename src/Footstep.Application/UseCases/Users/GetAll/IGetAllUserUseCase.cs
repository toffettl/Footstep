using Footstep.Communication.Responses.Traces;
using Footstep.Communication.Responses.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public interface IGetAllUserUseCase
    {
        Task<ResponseUsersJson> Execute();
    }
}
