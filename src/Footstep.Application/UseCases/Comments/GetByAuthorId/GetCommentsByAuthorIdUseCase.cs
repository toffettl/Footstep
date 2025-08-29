using AutoMapper;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.GetByAuthorId
{
    public class GetCommentsByAuthorIdUseCase : IGetCommentsByAuthorIdUseCase
    {
        private readonly ICommentsReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetCommentsByAuthorIdUseCase(ICommentsReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseCommentJson>> Execute(Guid id)
        {
            var result = await _repository.GetByUserId(id);

            if (result.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }
            return _mapper.Map<List<ResponseCommentJson>>(result);
        }
    }
}
