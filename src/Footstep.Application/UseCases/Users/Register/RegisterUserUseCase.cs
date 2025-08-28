using AutoMapper;
using Footstep.Domain.Repositories.Users;
using Footstep.Domain.Repositories;
using Footstep.Domain.Security.Cryptography;
using Footstep.Domain.Security.Tokens;
using Footstep.Communication.Responses.Users;
using Footstep.Exception;
using FluentValidation.Results;
using Footstep.Exception.ExceptionsBase;
using Footstep.Domain.Entities;
using Footstep.Communication.Requests.Users;
using Footstep.Domain.Repositories.Styles;
using Footstep.Communication.Enums;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepostory;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepostory;
    private readonly IStyleReadOnlyRepository _styleReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _tokenGenerator;
    public RegisterUserUseCase(
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepostory,
        IUserWriteOnlyRepository userWriteOnlyRepostory,
        IStyleReadOnlyRepository styleReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator tokenGenerator)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepostory = userReadOnlyRepostory;
        _userWriteOnlyRepostory = userWriteOnlyRepostory;
        _styleReadOnlyRepository = styleReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);

        user.Password = _passwordEncripter.Encrypt(request.Password!);
        user.Preference.MapStyle = request.MapStyle;

        var styles = await _styleReadOnlyRepository.GetAll();

        foreach(var style in styles)
        {
            Item item = new Item
            {
                Unblocked = false,
                Equipped = false,
                StyleId = style.Id,
            };

            user.Preference.Items.Add(item);
        }

        await _userWriteOnlyRepostory.Add(user);

        await _unitOfWork.Commit();

        return new ResponseUserJson
        {
            Token = _tokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _userReadOnlyRepostory.ExistActiveUserWithEmail(request.Email!);

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
