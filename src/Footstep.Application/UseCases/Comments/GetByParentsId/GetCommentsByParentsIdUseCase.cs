using AutoMapper;
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

        public async Task<List<ResponseCommentJson>> Execute(Guid id, int type)
        {
            var result = await _repository.GetByPointOfInterestId(id);

            if (type == 1)
            {
                result = await _repository.GetByCommentId(id);
            }

            if (result.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.COMMENT_NOT_FOUND);
            }
            return _mapper.Map<List<ResponseCommentJson>>(result);
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
