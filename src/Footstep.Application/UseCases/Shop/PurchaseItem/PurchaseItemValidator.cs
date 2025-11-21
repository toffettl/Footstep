using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Footstep.Communication.Requests.Shop;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Shop.PurchaseItem
{
    public class PurchaseItemValidator : AbstractValidator<RequestPurchaseItemJson>
    {
        public PurchaseItemValidator()
        {
            RuleFor(request => request.ItemId)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.ITEM_ID_REQUIRED);

            RuleFor(request => request.UserId)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.USER_ID_REQUIRED);
        }
    }
}
