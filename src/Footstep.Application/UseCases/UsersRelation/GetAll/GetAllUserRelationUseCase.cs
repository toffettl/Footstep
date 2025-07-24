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
    public class GetAllUserRelationUseCase : IGetAllUserRelationsUseCase
    {
        private readonly IUserRelationReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUserRelationUseCase(IUserRelationReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseAllRelationsJson>> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseUserRelationJson
            {
                Relations = _mapper.Map<List<ResponseUserRelationJson>>(result)
            };
        }
    }
}
