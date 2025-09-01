using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses.Users;

namespace Footstep.Application.UseCases.Users.Login;
public interface IDoLoginUseCase
{
    Task<ResponseUserTokenJson> Execute(RequestLoginJson request);
}
