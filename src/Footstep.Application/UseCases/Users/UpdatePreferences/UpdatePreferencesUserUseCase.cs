using AutoMapper;
using Footstep.Communication.Requests.Users;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Users.UpdatePreferences
{
    public class UpdatePreferencesUserUseCase : IUpdatePreferencesUserUseCase
    {
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePreferencesUserUseCase(IUserUpdateOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(Guid Id, RequestUpdatePreferencesUserJson request)
        {
            Validate(request);

            var user = await _repository.GetById(Id);

            if (user == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            _mapper.Map(request, user);
            user.UpdatedAt = DateTime.UtcNow;

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        public void Validate(RequestUpdatePreferencesUserJson request)
        {
            var validator = new PreferencesValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorsMessages);
            }
        }
    }
}
