using AutoMapper;
using Footstep.Communication.Requests.Styles;
using Footstep.Communication.Responses.Styles;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Styles;

namespace Footstep.Application.UseCases.Styles.Create
{
    public class CreateStyleUseCase : ICreateStyleUseCase
    {
        private readonly IStyleWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStyleUseCase(IStyleWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseStyleJson> Execute(RequestStyleJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Style>(request);


            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseStyleJson>(entity);
        }

        private void Validate(RequestStyleJson request)
        {
            var validator = new StyleValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new Exception.ExceptionsBase.ErrorOnValidationException(errorMessages);
            }
        }
    }
}
