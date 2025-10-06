namespace Footstep.Application.UseCases.Users.GetEmailExistence
{
    public interface IGetUserEmailExistenceUseCase
    {
        Task<bool> Execute(string email);
    }
}
