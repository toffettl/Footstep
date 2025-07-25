using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Traces;
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

        public async Task<ResponseUsersJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseUsersJson
            {
                Users = _mapper.Map<List<ResponseGetUserJson>>(result)
            };
        }
    }
}
