using AutoMapper;
using Footstep.Communication.Enums;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.GetByParentsId
{
    public class GetCommentsByParentIdUseCase : IGetCommentsByParentIdUseCase
    {
        private readonly ICommentsReadOnlyRepository _commentReadOnlyRepository;
        private readonly IMapper _mapper;
        public GetCommentsByParentIdUseCase(ICommentsReadOnlyRepository commrnyReadOnlyRepository,
            IMapper mapper)
        {
            _commentReadOnlyRepository = commrnyReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ResponseCommentJson>> Execute(Guid parentId, ParentType parentType, int page, int pageSize)
        {
            Validate(parentType);

            var (comments, totalCount) = await _commentReadOnlyRepository.GetByPointOfInterestId(parentId, page, pageSize);

            if (parentType == ParentType.Comment)
            {
                (comments, totalCount) = await _commentReadOnlyRepository.GetByCommentId(parentId, page, pageSize);
            }

            if (comments.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }

            List<ResponseCommentJson> responses = _mapper.Map<List<ResponseCommentJson>>(comments);

            foreach (var response in responses)
            {
                response.ParentId = parentId;
            }

            return new PagedResult<ResponseCommentJson>
            {
                Items = responses,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
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
