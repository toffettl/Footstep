using AutoMapper;
using Footstep.Communication.Requests.Marks;
using Footstep.Communication.Responses.Marks;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Marks;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Marks.Create
{
    public class CreateMarkUseCase : ICreateMarkUseCase
    {
        private readonly IMarkWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMarkUseCase(
                IMarkWriteOnlyRepository repository,
                IUnitOfWork unitOfWork,
                IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseMarkJson> Execute(RequestMarkJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Mark>(request);

            await _repository.Add(entity);
            
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseMarkJson>(entity);
        }

        private void Validate(RequestMarkJson request)
        {
            var validator = new MarkValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
