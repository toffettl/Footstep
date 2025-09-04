using AutoMapper;
using Footstep.Communication.Enums;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.GetByAuthorId
{
    public class GetCommentsByUserIdUseCase : IGetCommentsByUserIdUseCase
    {
        private readonly ICommentsReadOnlyRepository _commentReadOnlyRepository;
        private readonly IMapper _mapper;
        public GetCommentsByUserIdUseCase(ICommentsReadOnlyRepository repository,
            IMapper mapper)
        {
            _commentReadOnlyRepository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ResponseCommentJson>> Execute(Guid id, int page, int pageSize)
        {
            var (comments, totalCount) = await _commentReadOnlyRepository.GetByUserId(id, page, pageSize);

            if (comments.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }

            List<ResponseCommentJson> responses = new List<ResponseCommentJson>();

            foreach (var comment in comments)
            {
                ResponseCommentJson response = _mapper.Map<ResponseCommentJson>(comment);

                switch ((ParentType)(int)comment.ParentType)
                {
                    case ParentType.PointOfInterest:
                        response.ParentId = comment.ParentPointOfInterestId;
                        break;
                    case ParentType.Comment:
                        response.ParentId = comment.ParentCommentId;
                        break;
                }

                responses.Add(response);
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
    }
}
