using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.GetByParentsId
{
    public class GetCommentsByParentsIdUseCase : IGetCommentsByParentsIdUseCase
    {
        private readonly ICommentsReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetCommentsByParentsIdUseCase(ICommentsReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ResponseCommentJson>> Execute(Guid id, int page, int pageSize)
        {
            var (comments, totalCount) = await _repository.GetByParentsId(id, page, pageSize);

            if (comments.Count == 0)
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);

            return new PagedResult<ResponseCommentJson>
            {
                Items = _mapper.Map<List<ResponseCommentJson>>(comments),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
    }
}
