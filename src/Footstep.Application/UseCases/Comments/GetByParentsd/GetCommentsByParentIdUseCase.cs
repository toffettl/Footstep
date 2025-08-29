using AutoMapper;
using Footstep.Communication.Enums;
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

        public async Task<List<ResponseCommentJson>> Execute(Guid parentId, ParentType parentType)
        {
            var comments = await _commentReadOnlyRepository.GetByPointOfInterestId(parentId);

            if (parentType == ParentType.Comment)
            {
                comments = await _commentReadOnlyRepository.GetByCommentId(parentId);
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

            return responses;
        }
    }
}
