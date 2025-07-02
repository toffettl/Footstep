using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses.Users;

namespace Footstep.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
