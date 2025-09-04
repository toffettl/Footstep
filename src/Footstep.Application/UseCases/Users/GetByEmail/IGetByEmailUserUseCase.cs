using Footstep.Communication.Responses.Users;

namespace Footstep.Application.UseCases.Users.GetByEmail
{
    public interface IGetByEmailUserUseCase
    {
        Task<ResponseUserJson> Execute(string email);
    }
}
