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

        public async Task<List<ResponseCommentJson>> Execute(Guid parentId, Guid authorId, int type)
        {
            Validate(type);

            var response = await _repository.GetByPointOfInterestIdAndAuthorId(parentId, authorId);

            if (type ==1)
            {
                response = await _repository.GetByCommentIdAndAuthorId(parentId, authorId);
            }

            if (response.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }

            return _mapper.Map<List<ResponseCommentJson>>(response);
        }

        private void Validate(int type)
        {
            var validator = new TypeValidator();

            var result = validator.Validate(type);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
