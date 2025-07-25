using Footstep.Communication.Requests.Users;

namespace Footstep.Application.UseCases.Users.UpdateUnlockedStyles
{
    public interface IUpdateUnlockedStylesUserUseCase
    {
        Task Execute(Guid id, RequestUpdateUnlockedStylesUserJson request);
    }
}
