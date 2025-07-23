using AutoMapper;
using Footstep.Communication.Requests.Comments;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.Update
{
    public class UpdateStatusCommentsUseCase : IUpdateStatusCommentsUseCase
    {
        private readonly ICommentsUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStatusCommentsUseCase(ICommentsUpdateOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(Guid id, RequestUpdateStatusCommentsJson request)
        {
            var comment = await _repository.GetById(id);

            if (comment == null)
            {
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }

            _mapper.Map(request, comment);

            comment.UpdatedAt = DateTime.UtcNow;
            _repository.Update(comment);

            await _unitOfWork.Commit();
        }
    }
}
