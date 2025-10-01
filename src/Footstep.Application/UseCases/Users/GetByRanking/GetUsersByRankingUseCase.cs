using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;

namespace Footstep.Application.UseCases.Users.GetByRanking
{
    public class GetUsersByRankingUseCase : IGetUsersByRankingUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetUsersByRankingUseCase(IUserReadOnlyRepository userReadOnlyRepository,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ResponsePaginationUserJson>> Execute(int page, int pageSize, DateTime dateTime)
        {
            var (users, totalCount) = await _userReadOnlyRepository.GetByRanking(page, pageSize, dateTime);

            List<ResponsePaginationUserJson> responses = _mapper.Map<List<ResponsePaginationUserJson>>(users);

            return new PagedResult<ResponsePaginationUserJson>
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
