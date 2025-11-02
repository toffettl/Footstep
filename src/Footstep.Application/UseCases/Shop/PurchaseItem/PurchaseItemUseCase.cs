using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Communication.Requests.Shop;
using Footstep.Communication.Responses.Shop;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Coins;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.UserItem;
using Footstep.Domain.Repositories.UserItems;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace Footstep.Application.UseCases.Shop.PurchaseItem
{
    public class PurchaseItemUseCase
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly ICoinReadOnlyRepository _coinRepository;
        private readonly ICoinUpdateOnlyRepository _coinUpdateRepository;
        private readonly IItemReadOnlyRepository _itemRepository;
        private readonly IItemWriteOnlyRepository _itemWriteRepository;
        private readonly IItemUpdateOnlyRepository _itemUpdateRepository;
        private readonly IUserItemReadOnlyRepository _userItemRepository;
        private readonly IUserItemWriteOnlyRepository _userItemWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseItemUseCase(
            IUserReadOnlyRepository userRepository,
            ICoinReadOnlyRepository coinRepository,
            ICoinUpdateOnlyRepository coinUpdateRepository,
            IItemReadOnlyRepository itemRepository,
            IItemWriteOnlyRepository itemWriteRepository,
            IItemUpdateOnlyRepository itemUpdateRepository,
            IUserItemReadOnlyRepository userItemRepository,
            IUserItemWriteOnlyRepository userItemWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _coinRepository = coinRepository;
            _coinUpdateRepository = coinUpdateRepository;
            _itemRepository = itemRepository;
            _itemWriteRepository = itemWriteRepository;
            _itemUpdateRepository = itemUpdateRepository;
            _userItemRepository = userItemRepository;
            _userItemWriteRepository = userItemWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponsePurchaseItemJson> Execute(RequestPurchaseItemJson request)
        {
            await Validate(request);

            var coin = await _coinRepository.GetByUserId(request.UserId);
            if (coin == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_COINS_NOT_FOUND);

            }

            var item = await _itemRepository.GetById(request.ItemId);
            if ( item == null || !item.IsAvaliableInShop)
            {
                throw new NotFoundException(ResourceErrorMessages.ITEM_NOT_AVAILABLE);
            }

            var alreadyPurchased = await _userItemRepository.HasUserPurchasedAsync(request.UserId, request.ItemId);
            if (alreadyPurchased)
            {
                throw new ErrorOnValidationException(new List<string> { ResourceErrorMessages.ITEM_ALREADY_PURCHASED });
            }
            if (coin.Total < item.Price)
            {
                throw new ErrorOnValidationException(new List<string> { ResourceErrorMessages.INSUFFICIENT_COINS });
            }

            coin.Total -= item.Price;
            coin.Spent += item.Price;
            item.Unlocked = true;

            var userItem = new UserItem
            {
                UserId = request.UserId,
                ItemId = request.ItemId,
                PurchasedAt = DateTime.UtcNow
            };

            await _userItemWriteRepository.Add(userItem);
            _coinUpdateRepository.Update(coin);
            _itemUpdateRepository.Update(item);

            return new ResponsePurchaseItemJson
            {
                Success = true,
                Message = "Item purchase is successfull",
                RemainingCoins = coin.Total,
                PurchasedItemId = userItem.ItemId
            };

        }

        private async Task Validate(RequestPurchaseItemJson request)
        {
            var result = new PurchaseItemValidator().Validate(request);
            var userExists = await _userRepository.ExistActiveUserWithId(request.UserId);
            if (!userExists)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.USER_NOT_FOUND));

            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
