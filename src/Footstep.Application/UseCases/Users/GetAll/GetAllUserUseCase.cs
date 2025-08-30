using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public class GetAllUserUseCase : IGetAllUserUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUserUseCase(IUserReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseUserJson>> Execute()
        {
            var users = await _repository.GetAll();

            return _mapper.Map<List<ResponseUserJson>>(users);
        }
    }
}
