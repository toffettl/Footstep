using AutoMapper;
using Footstep.Communication.Requests.Comments;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Comments.UpdateContent
{
    public class UpdateContentCommentUseCase : IUpdateContentCommentUseCase
    {
        private readonly ICommentsReadOnlyRepository _commentReadOnlyRepository;
        private readonly ICommentsUpdateOnlyRepository _commentUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateContentCommentUseCase(
            ICommentsReadOnlyRepository commentsReadOnlyRepository,
            ICommentsUpdateOnlyRepository commentsUpdateOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _commentReadOnlyRepository = commentsReadOnlyRepository;
            _commentUpdateOnlyRepository = commentsUpdateOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(Guid id, RequestUpdateContentComment request)
        {
            Validate(request);

            var comment = await _commentReadOnlyRepository.GetById(id);

            if (comment == null)
            {
                throw new EntryPointNotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }

            _mapper.Map(request, comment);

            comment.UpdatedAt = DateTime.UtcNow;

            _commentUpdateOnlyRepository.Update(comment);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestUpdateContentComment request)
        {
            var validator = new ContentValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(erroMessages);
            }
        }
    }
}
