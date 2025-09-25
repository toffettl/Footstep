using AutoMapper;
using Footstep.Communication.Responses.Styles;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Styles;

namespace Footstep.Application.UseCases.Styles.GetByStore
{
    public class GetByStoreStyleUseCase : IGetByStoreStyleUseCase
    {
        private readonly IStyleReadOnlyRepository _styleReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetByStoreStyleUseCase(
            IStyleReadOnlyRepository styleReadOnlyRepository,
            IMapper mapper)
        {
            _styleReadOnlyRepository = styleReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<ResponseStyleJson>> Execute()
        {
            var styles = await _styleReadOnlyRepository.GetByStore();

            return _mapper.Map<List<ResponseStyleJson>>(styles);
        }
    }
}
