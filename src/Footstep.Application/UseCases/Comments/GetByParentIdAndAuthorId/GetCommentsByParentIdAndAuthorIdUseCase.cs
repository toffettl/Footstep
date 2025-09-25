using AutoMapper;
using Footstep.Communication.Enums;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.GetByParentIdAndAuthorId
{
    public class GetCommentsByParentIdAndAuthorIdUseCase : IGetCommentsByParentIdAndAuthorIdUseCase
    {
        private readonly ICommentsReadOnlyRepository _commentReadOnlyRepository;
        private readonly IMapper _mapper;
        public GetCommentsByParentIdAndAuthorIdUseCase(ICommentsReadOnlyRepository commentReadOnlyRepository,
            IMapper mapper)
        {
            _commentReadOnlyRepository = commentReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<ResponseCommentJson>> Execute(Guid parentId, Guid authorId, ParentType parentType)
        {
            Validate(parentType);

            var comment = await _commentReadOnlyRepository.GetByPointOfInterestIdAndUserId(parentId, authorId);

            if (parentType == ParentType.Comment)
            {
                comment = await _commentReadOnlyRepository.GetByCommentIdAndUserId(parentId, authorId);
            }

            if (comment.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }

            List<ResponseCommentJson> responses = _mapper.Map<List<ResponseCommentJson>>(comment);

            foreach (var response in responses)
            {
                response.ParentId = parentId;
            }

            return responses;
        }

        private void Validate(ParentType parentType)
        {
            var validator = new ParentTypeValidator();

            var result = validator.Validate(parentType);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
