using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Footstep.Communication.Responses.UserRelation;
using Footstep.Domain.Repositories.RelationUser;

namespace Footstep.Application.UseCases.UsersRelation.GetAll
{
    public class GetAllUsersRelationsUseCase : IGetAllUserRelationsUseCase
    {
        private readonly IUserRelationReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUsersRelationsUseCase(IUserRelationReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseUsersRelationsJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseUsersRelationsJson
            {
                Relations = _mapper.Map<List<ResponseUserRelationJson>>(result)
            };
        }

    }
}
