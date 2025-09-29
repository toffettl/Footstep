using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public class GetAllUserPaginationUseCase : IGetAllUserPaginationUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetAllUserPaginationUseCase(IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ResponsePaginationUserJson>> Execute(int page, int pageSize)
        {
            var (users, totalCount) = await _userReadOnlyRepository.GetAllPagination(page, pageSize);

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
