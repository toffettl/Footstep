using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Enums;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Users.GetById
{
    public class GetByIdUserUseCase : IGetByIdUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IItemReadOnlyRepository _itemReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetByIdUserUseCase(
            IUserReadOnlyRepository userReadOnlyRepository, 
            IItemReadOnlyRepository itemReadOnlyRepository,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _itemReadOnlyRepository = itemReadOnlyRepository;
            _mapper = mapper;
        }
            
        public async Task<ResponseUserJson> Execute(Guid id)
        {
            var user = await _userReadOnlyRepository.GetById(id);

            if (user == null)
            {
                throw new DirectoryNotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            return _mapper.Map<ResponseUserJson>(user);
        }
    }
}
