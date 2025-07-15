using AutoMapper;
using Footstep.Application.UseCases.Marks.Get;
using Footstep.Communication.Responses.Marks;
using Footstep.Domain.Repositories.Marks;

namespace Footstep.Application.UseCases.Marks.GetAll
{
    public class GetAllMarkUseCase : IGetAllMarkUseCase
    {
        private readonly IMarkReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMarkUseCase(IMarkReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseMarksJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseMarksJson
            {
                Marks = _mapper.Map<List<ResponseMarkJson>>(result)
            };
        }
    }
}
