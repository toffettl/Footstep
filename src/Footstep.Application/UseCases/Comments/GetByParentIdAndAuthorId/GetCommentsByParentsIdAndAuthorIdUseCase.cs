using AutoMapper;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.GetByParentIdAndAuthorId
{
    public class GetCommentsByParentsIdAndAuthorIdUseCase : IGetCommentsByParentsIdAndAuthorIdUseCase
    {
        private readonly ICommentsReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetCommentsByParentsIdAndAuthorIdUseCase(ICommentsReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseCommentJson>> Execute(Guid parentId, Guid authorId)
        {
            var result = await _repository.GetByParentIdAndAuthorId(parentId, authorId);

            if (result.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }
            return _mapper.Map<List<ResponseCommentJson>>(result);
        }
    }
}
