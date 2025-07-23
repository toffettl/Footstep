using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;
using Footstep.Domain.Security.Cryptography;
using Footstep.Domain.Security.Tokens;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Users.Login;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmail(request.Email!);

        if (user == null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password!, user.Password!);

        if (passwordMatch == false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}
