using Footstep.Communication.Requests.Comments;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.Delete
{
    public class DeleteCommentUseCase : IDeleteCommentUseCase
    {
        private readonly ICommentsWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommentUseCase(
            ICommentsWriteOnlyRepository repository,
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
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
