using AutoMapper;
using Footstep.Communication.Responses.Styles;
using Footstep.Domain.Repositories.Styles;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Styles.GetByName
{
    public class GetByNameStyleUseCase : IGetByNameStyleUseCase
    {
        private readonly IStyleReadOnlyRepository _styleReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetByNameStyleUseCase(
            IStyleReadOnlyRepository styleReadOnlyRepository,
            IMapper mapper)
        {
            _styleReadOnlyRepository = styleReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseStyleJson> Execute(string name)
        {
            var style = await _styleReadOnlyRepository.GetByName(name);

            if (style == null)
            {
                throw new DirectoryNotFoundException(ResourceErrorMessages.STYLE_NOT_FOUND);
            }

            return _mapper.Map<ResponseStyleJson>(style);
        }
    }
}
