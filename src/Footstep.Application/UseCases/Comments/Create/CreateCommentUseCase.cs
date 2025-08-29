using AutoMapper;
using Footstep.Communication.Enums;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.Create
{
    public class CreateCommentUseCase : ICreateCommentUseCase
    {
        private readonly ICommentsWriteOnlyRepository _CommentWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCommentUseCase(
            ICommentsWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _CommentWriteOnlyRepository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseCommentJson> Execute(RequestCommentJson request)
        {
            Validate(request);

            var comment = _mapper.Map<Comment>(request);

            switch ((ParentType)(int)comment.ParentType)
            {
                case ParentType.PointOfInterest:
                    comment.ParentPointOfInterestId = request.ParentId;
                    comment.ParentCommentId = null;
                    break;
                case ParentType.Comment:
                    comment.ParentCommentId = request.ParentId;
                    comment.ParentPointOfInterestId = null;
                    break;
            }

            CommentLike commentLike = new CommentLike
            {
                CommentId = comment.Id,
                UserId = comment.UserId
            };

            comment.CommentLikes.Add(commentLike);

            await _CommentWriteOnlyRepository.Add(comment);

            await _unitOfWork.Commit();

            ResponseCommentJson response = _mapper.Map<ResponseCommentJson>(comment);

            response.ParentId = request.ParentId;
            return response;
        }

        private void Validate(RequestCommentJson request)
        {
            var validator = new CommentValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
