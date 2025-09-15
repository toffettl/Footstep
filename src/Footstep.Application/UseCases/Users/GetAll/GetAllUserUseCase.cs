using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public class GetAllUserUseCase : IGetAllUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IItemReadOnlyRepository _itemReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetAllUserUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IItemReadOnlyRepository itemReadOnlyRepository,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _itemReadOnlyRepository = itemReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<ResponseUserJson>> Execute()
        {
            var users = await _userReadOnlyRepository.GetAll();

            return new List<ResponseUserJson>();
        }
    }
}
