using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Footstep.Communication.Responses.Shop;
using Footstep.Domain.Repositories.Coins;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Shop.GetUserCoins
{
    public class GetUserCoinUseCase : IGetUserCoinsUseCase
    {
        private readonly ICoinReadOnlyRepository _coinRepository;
        private readonly IMapper _mapper;

        public GetUserCoinUseCase(
            ICoinReadOnlyRepository coinRepository,
            IMapper mapper)
        {
            _coinRepository = coinRepository;
            _mapper = mapper;
        }

        public async Task<ResponseUserCoinsJson> Execute(Guid userId)
        {
            var coin = await _coinRepository.GetByUserIdAsync(userId);

            if (coin == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_COINS_NOT_FOUND)
            }

            var response = _mapper.Map<ResponseUserCoinsJson>(coin);
            response.UserId = userId;

            return response;
        }
    }
}
