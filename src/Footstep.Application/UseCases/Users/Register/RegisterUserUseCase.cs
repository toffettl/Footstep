using AutoMapper;
using FluentValidation.Results;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Preferences;
using Footstep.Domain.Repositories.Styles;
using Footstep.Domain.Repositories.Users;
using Footstep.Domain.Security.Cryptography;
using Footstep.Domain.Security.Tokens;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IPreferenceWriteOnlyRepository _preferenceWriteOnlyRepository;
    private readonly IStyleWriteOnlyRepository _styleWriteOnlyRepository;
    private readonly IStyleReadOnlyRepository _styleReadOnlyRepository;
    private readonly IItemWriteOnlyRepository _itemWriteOnlyRepository;
    private readonly IAccessTokenGenerator _tokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterUserUseCase(
        IPasswordEncripter passwordEncripter,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IPreferenceWriteOnlyRepository preferenceWriteOnlyRepository,
        IStyleWriteOnlyRepository styleWriteOnlyRepository,
        IStyleReadOnlyRepository styleReadOnlyRepository,
        IItemWriteOnlyRepository itemWriteOnlyRepository,
        IAccessTokenGenerator tokenGenerator,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _passwordEncripter = passwordEncripter;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _preferenceWriteOnlyRepository = preferenceWriteOnlyRepository;
        _styleWriteOnlyRepository = styleWriteOnlyRepository;
        _styleReadOnlyRepository = styleReadOnlyRepository;
        _itemWriteOnlyRepository = itemWriteOnlyRepository;
        _tokenGenerator = tokenGenerator;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseUserTokenJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);

        user.Password = _passwordEncripter.Encrypt(request.Password!);
        user.PreferenceId = user.Preference.Id;
        user.CoinId = user.Coin.Id;
        user.Preference.MapStyle = "";
        user.Preference.UnlockedMapStyles = "";

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseUserTokenJson
        {
            Token = _tokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);
        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email!);

        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
