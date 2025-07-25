using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Users.GetById
{
    public class GetByIdUserUseCase : IGetByIdUserUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdUserUseCase(IUserReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
            
        public async Task<ResponseGetUserJson> Execute(Guid id)
        {
            var result = await _repository.GetById(id);

            if (result == null)
            {
                throw new DirectoryNotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            return _mapper.Map<ResponseGetUserJson>(result);
        }
    }
}
