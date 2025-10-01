using AutoMapper;
using FluentValidation.Results;
using Footstep.Communication.Enums;
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

        await _userWriteOnlyRepository.Add(user);
        await _preferenceWriteOnlyRepository.Add(user.Preference);

        await CreateItems(user.Preference.Id);

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

    private async Task CreateItems(Guid preferenceId)
    {
        var styles = await _styleReadOnlyRepository.GetAll();
        List<Style> basicStyles = new List<Style>();

        basicStyles.Add(await CreateBasicStyle(styles, "Basic Head", "Basic", StyleType.Head));
        basicStyles.Add(await CreateBasicStyle(styles, "Basic Body", "Basic", StyleType.Torso));
        basicStyles.Add(await CreateBasicStyle(styles, "Basic Leg", "Basic", StyleType.Leg));
        basicStyles.Add(await CreateBasicStyle(styles, "Basic Bag", "Basic", StyleType.Bag));
        basicStyles.Add(await CreateBasicStyle(styles, "Basic Accessory", "Basic", StyleType.Accessory));
        basicStyles.Add(await CreateBasicStyle(styles, "Basic PoitnOfInterest", "Basic", StyleType.PointOfInterest));

        styles.RemoveAll(s => basicStyles.Contains(s));

        foreach (var style in styles)
        {
            Item item = new Item
            {
                Unlocked = false,
                Equipped = false,
                StyleId = style.Id,
                PreferenceId = preferenceId
            };

            await _itemWriteOnlyRepository.Add(item);
        }

        foreach (var basicStyle in basicStyles)
        {
            Item item = new Item
            {
                Unlocked = true,
                Equipped = true,
                StyleId = basicStyle.Id,
                PreferenceId = preferenceId
            };

            await _itemWriteOnlyRepository.Add(item);
        }
    }

    private async Task<Style> CreateBasicStyle(List<Style> styles, string name, string image, StyleType styleType)
    {
        var style = styles.FirstOrDefault(s => s.Name == name);

        if (style == null)
        {
            style = new Style
            {
                Name = name,
                Image = image,
                Price = 0,
                Store = false,
                StyleType = (Domain.Enums.StyleType)(int)styleType
            };

            await _styleWriteOnlyRepository.Add(style);
        }

        return style;
    }
}
