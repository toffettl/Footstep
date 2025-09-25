using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Traces.GetAllByPage
{
    public class GetAllPointsOfInterestByPageUseCase : IGetAllPointsOfInterestByPageUseCase
    {
        private readonly IPointOfInterestReadOnlyRepository _pointsOfInterestReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetAllPointsOfInterestByPageUseCase(
            IPointOfInterestReadOnlyRepository pointsOfInterestReadOnlyRepository,
            IMapper mapper)
        {
            _pointsOfInterestReadOnlyRepository = pointsOfInterestReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ResponsePaginationPointOfInterestJson>> Execute(int page, int pageSize)
        {
            var (pointsOfInterest, totalCount) = await _pointsOfInterestReadOnlyRepository.GetAllByPage(page, pageSize);

            var responses = _mapper.Map<List<ResponsePaginationPointOfInterestJson>>(pointsOfInterest);

            return new PagedResult<ResponsePaginationPointOfInterestJson>
            {
                Items = responses,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }
    }
}
