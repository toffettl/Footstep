using AutoMapper;
using Footstep.Communication.Responses.Marks;
using Footstep.Domain.Repositories.Marks;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Marks.GetById
{
    public class GetByIdMarkUseCase : IGetByIdMarkUseCase
    {
        private readonly IMarkReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdMarkUseCase(IMarkReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseMarkJson> Execute(Guid id)
        {
            var result = await _repository.GetById(id);
            
            if(result == null) {
                throw new NotFoundException(ResourceErrorMessages.MARK_NOT_FOUND);
            }

            return _mapper.Map<ResponseMarkJson>(result);
        }
    }
}
