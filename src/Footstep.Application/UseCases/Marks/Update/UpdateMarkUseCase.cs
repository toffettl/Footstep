using AutoMapper;
using Footstep.Communication.Requests.Marks;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Marks;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Marks.Update
{
    public class UpdateMarkUseCase : IUpdateMarkUseCase
    {
        private readonly IMarkUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMarkUseCase(IMarkUpdateOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(Guid id, RequestMarkJson request)
        {
            Validate(request);

            var mark = await _repository.GetById(id);

            if (mark == null)
            {
                throw new NotFoundException(ResourceErrorMessages.MARK_NOT_FOUND);
            }

            _mapper.Map(request, mark);

            _repository.Update(mark);

            await _unitOfWork.Commit();
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
