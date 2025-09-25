using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.Delete
{
    public class DeletePointOfInterestUseCase : IDeletePointOfInterestUseCase
    {
        private readonly IPointOfInterestWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeletePointOfInterestUseCase(IPointOfInterestWriteOnlyRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid id)
        {
            var result = await _repository.Delete(id);

            if (result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.TRACE_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
