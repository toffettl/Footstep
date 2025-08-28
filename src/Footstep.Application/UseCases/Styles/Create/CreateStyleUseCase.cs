using AutoMapper;
using FluentValidation.Results;
using Footstep.Communication.Requests.Styles;
using Footstep.Communication.Responses.Styles;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Preferences;
using Footstep.Domain.Repositories.Styles;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Styles.Create
{
    public class CreateStyleUseCase : ICreateStyleUseCase
    {
        private readonly IStyleWriteOnlyRepository _styleOnlyWriteRepository;
        private readonly IStyleReadOnlyRepository _styleReadOnlyRepository;
        private readonly IPreferenceReadOnlyRepository _preferenceReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStyleUseCase(
            IStyleWriteOnlyRepository styleWriteOnlyRepository,
            IStyleReadOnlyRepository styleReadOnlyRepository,
            IPreferenceReadOnlyRepository preferenceReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _styleOnlyWriteRepository = styleWriteOnlyRepository;
            _styleReadOnlyRepository = styleReadOnlyRepository;
            _preferenceReadOnlyRepository = preferenceReadOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseStyleJson> Execute(RequestStyleJson request)
        {
            await Validate(request);

            var style = _mapper.Map<Style>(request);

            var preferences = await _preferenceReadOnlyRepository.GetAll();

            foreach (var preference in preferences)
            {
                Item item = new Item
                {
                    Unblocked = false,
                    Equipped = false,
                    PreferenceId = preference.Id,
                };

                style.Items.Add(item);
            }

            await _styleOnlyWriteRepository.Add(style);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseStyleJson>(style);
        }

        private async Task Validate(RequestStyleJson request)
        {
            var validator = new StyleValidator();

            var result = validator.Validate(request);

            var  nameExists = await _styleReadOnlyRepository.GetByName(request.Name);

            if (nameExists != null)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.NAME_ALREADY_REGISTERED));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
