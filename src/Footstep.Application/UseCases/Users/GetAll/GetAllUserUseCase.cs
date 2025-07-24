using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{



    public class GetAllUserUseCase : IGetAllUserUseCase
    {

        private readonly IUserReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUserUseCase(IUserReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseUsersJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseUsersJson
            {
                Users = _mapper.Map<List<ResponseUserJson>>(result),
            };
        }

        
    }
}
