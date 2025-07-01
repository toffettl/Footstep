using AutoMapper;
using Footstep.Domain.Repositories.Users;
using Footstep.Domain.Repositories;
using Footstep.Domain.Security.Cryptography;
using Footstep.Domain.Security.Tokens;
using Footstep.Communication.Responses.Users;
using Footstep.Communication.Requests;
using Footstep.Exception;
using FluentValidation.Results;
using Footstep.Exception.ExceptionsBase;
using Footstep.Domain.Entities;

namespace Footstep.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepostory;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepostory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _tokenGenerator;
    public RegisterUserUseCase(
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepostory,
        IUserWriteOnlyRepository userWriteOnlyRepostory,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator tokenGenerator)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepostory = userReadOnlyRepostory;
        _userWriteOnlyRepostory = userWriteOnlyRepostory;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validade(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password!);
        user.Id = Guid.NewGuid();

        await _userWriteOnlyRepostory.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.Generate(user)
        };
    }

    private async Task Validade(RequestRegisterUserJson request)
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
