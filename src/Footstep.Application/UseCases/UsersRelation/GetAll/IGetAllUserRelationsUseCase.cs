using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Communication.Responses.UserRelation;

namespace Footstep.Application.UseCases.UsersRelation.GetAll
{
    public interface IGetAllUserRelationsUseCase
    {
        Task<ResponseUsersRelationsJson> Execute();
    }
}
