using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Communication.Responses.Shop;

namespace Footstep.Application.UseCases.Shop.GetUserCoins
{
    public interface IGetUserCoinsUseCase
    {
        Task<ResponseUserCoinsJson> Execute(Guid userId);
    }
}
