using Footstep.Communication.Responses.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public interface IGetAllUserUseCase
    {
        Task<List<ResponseUserTokenJson>> Execute();
    }
}
