using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Footstep.Communication.Responses.Shop;
using Footstep.Domain.Repositories.Items;

namespace Footstep.Application.UseCases.Shop.GetAvailableItems
{
    public class GetAvaliableItemsUseCase
    {
        private readonly IItemReadOnlyRepository _itemRepository;
        private readonly IMapper _mapper;

        public GetAvaliableItemsUseCase(IItemReadOnlyRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<List<ResponseShopItemJson>> Execute(Guid userId)
        {
            var items = await _itemRepository.GetAvailableForUser(userId);
            return _mapper.Map<List<ResponseShopItemJson>>(items);
        }
    }
}
