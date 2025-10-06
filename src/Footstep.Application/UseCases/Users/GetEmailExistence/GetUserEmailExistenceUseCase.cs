using Footstep.Domain.Repositories.Users;

namespace Footstep.Application.UseCases.Users.GetEmailExistence
{
    public class GetUserEmailExistenceUseCase : IGetUserEmailExistenceUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public GetUserEmailExistenceUseCase(IUserReadOnlyRepository userReadOnlyRepository)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<bool> Execute(string email)
        {
            return await _userReadOnlyRepository.ExistActiveUserWithEmail(email);
        }
    }
}
