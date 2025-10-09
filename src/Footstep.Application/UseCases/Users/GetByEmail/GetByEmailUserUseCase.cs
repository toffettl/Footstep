using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Enums;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Users.GetByEmail
{
    public class GetByEmailUserUseCase : IGetByEmailUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IItemReadOnlyRepository _itemReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetByEmailUserUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IItemReadOnlyRepository itemReadOnlyRepository,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _itemReadOnlyRepository = itemReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseUserJson> Execute(string email)
        {
            var user = await _userReadOnlyRepository.GetByEmail(email);

            if (user == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            return _mapper.Map<ResponseUserJson>(user);
        }
    }
}
