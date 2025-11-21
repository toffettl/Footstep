namespace Footstep.Application.UseCases.Users.UpdateAddProfilePicture
{
    public interface IUpdateAddProfilePictureUserUseCase
    {
        Task Execute(Guid Id, Stream stream, string fileName, string contentType);
    }
}
