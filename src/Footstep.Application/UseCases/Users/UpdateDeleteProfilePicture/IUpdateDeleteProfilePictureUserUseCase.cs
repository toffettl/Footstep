namespace Footstep.Application.UseCases.Users.UpdateDeleteProfilePicture
{
    public interface IUpdateDeleteProfilePictureUserUseCase
    {
        Task Execute(Guid id);
    }
}
