using Footstep.Communication.Responses.Users;

namespace Footstep.Application.UseCases.Users.GetByEmail
{
    public interface IGetByEmailUserUseCase
    {
        Task<ResponseGetUserJson> Execute(string email);
    }
}
