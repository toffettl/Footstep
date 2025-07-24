using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Communication.Responses.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public interface IGetAllUserUseCase
    {
        Task<ResponseUserJson> Execute();
    }
}
