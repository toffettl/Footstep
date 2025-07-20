using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Users.GetByEmail
{
    public class GetByEmailUserUseCase : IGetByEmailUserUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetByEmailUserUseCase(IUserReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseGetUserJson> Execute(string email)
        {
            var result = await _repository.GetUserByEmail(email);

            if (result == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            return _mapper.Map<ResponseGetUserJson>(result);
        }
    }
}
