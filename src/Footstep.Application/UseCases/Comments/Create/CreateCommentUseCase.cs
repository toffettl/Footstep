using AutoMapper;
using FluentValidation.Results;
using Footstep.Communication.Enums;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Comments;
using Footstep.Domain.Repositories.Traces;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.Create
{
    public class CreateCommentUseCase : ICreateCommentUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IPointOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
        private readonly ICommentsReadOnlyRepository _commentsReadOnlyRepository;
        private readonly ICommentsWriteOnlyRepository _commentWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCommentUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IPointOfInterestReadOnlyRepository pointOfInterestReadOnlyRepository,
            ICommentsReadOnlyRepository commentsReadOnlyRepository,
            ICommentsWriteOnlyRepository commentWriteOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _pointOfInterestReadOnlyRepository = pointOfInterestReadOnlyRepository;
            _commentsReadOnlyRepository = commentsReadOnlyRepository;
            _commentWriteOnlyRepository = commentWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseCommentJson> Execute(RequestCommentJson request)
        {
            await Validate(request);

            var comment = _mapper.Map<Comment>(request);

            switch ((ParentType)(int)comment.ParentType)
            {
                case ParentType.Mark:
                    if (await _pointOfInterestReadOnlyRepository.GetById(request.ParentId) == null)
                    {
                        throw new NotFoundException(ResourceErrorMessages.POINT_OF_INTEREST_NOT_FOUND);
                    }

                    comment.ParentPointOfInterestId = request.ParentId;
                    comment.ParentCommentId = null;
                    break;
                case ParentType.Comment:
                    if (await _commentsReadOnlyRepository.GetById(request.ParentId) == null)
                    {
                        throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
                    }

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

            await _commentWriteOnlyRepository.Add(comment);

            await _unitOfWork.Commit();

            ResponseCommentJson response = _mapper.Map<ResponseCommentJson>(comment);

            response.ParentId = request.ParentId;

            return response;
        }

        private async Task Validate(RequestCommentJson request)
        {
            var validator = new CommentValidator();

            var result = validator.Validate(request);

            var existsId = await _userReadOnlyRepository.ExistActiveUserWithId(request.AuthorId!);

            if (!existsId)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.USER_NOT_FOUND));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
