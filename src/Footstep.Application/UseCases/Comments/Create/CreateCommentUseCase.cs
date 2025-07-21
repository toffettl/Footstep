using AutoMapper;
using Footstep.Application.UseCases.Traces;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Comments;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Comments.Create
{
    public class CreateCommentUseCase : ICreateCommentUseCase
    {
        private readonly ICommentsWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCommentUseCase(
            ICommentsWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseCreateComments> Execute(RequestCommentJson request)
        {
            Validade(request);

            var entity = _mapper.Map<Comment>(request);
            
            entity.CreatedAt = DateTime.UtcNow;

            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseCreateComments>(entity);
        }

        private void Validade(RequestCommentJson request)
        {
            var validator = new CommentValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
