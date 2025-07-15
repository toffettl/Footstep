using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Marks;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Marks.Delete
{
    public class DeleteMarkUsecase : IDeleteMarkUseCase
    {
        private readonly IMarkWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMarkUsecase(IMarkWriteOnlyRepository repository,
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
                throw new NotFoundException(ResourceErrorMessages.MARK_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
