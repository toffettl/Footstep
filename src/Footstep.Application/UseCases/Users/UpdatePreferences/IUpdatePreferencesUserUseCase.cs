using Footstep.Communication.Requests.Users;

namespace Footstep.Application.UseCases.Users.UpdatePreferences
{
    public interface IUpdatePreferencesUserUseCase
    {
        Task Execute(Guid Id, RequestUpdatePreferencesUserJson request);
    }
}
