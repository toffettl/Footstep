using AutoMapper;
using Footstep.Communication.Requests.Users;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Users.UpdateUnlockedStyles
{
    public class UpdateUnlockedStylesUserUseCase : IUpdateUnlockedStylesUserUseCase
    {
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IItemReadOnlyRepository _itemReadOnlyRepository;
        private readonly IItemUpdateOnlyRepository _itemUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUnlockedStylesUserUseCase(
            IUserUpdateOnlyRepository userUpdateOnlyRepository,
            IItemReadOnlyRepository itemReadOnlyRepository,
            IItemUpdateOnlyRepository itemUpdateOnlyRepository,
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _userUpdateOnlyRepository = userUpdateOnlyRepository;
            _itemReadOnlyRepository = itemReadOnlyRepository;
            _itemUpdateOnlyRepository = itemUpdateOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(Guid id, RequestUpdateUnlockedStylesUserJson request)
        {
            Validate(request);

            var user = await _userUpdateOnlyRepository.GetById(id);

            if (user == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            var items = await _itemReadOnlyRepository.GetByPreferenceId(user.PreferenceId);

            await UpdateUnlockedItem(request.UnlockedPointOfInterestStyle!.Value, items);
            await UpdateUnlockedItem(request.UnlockedHeadStyle!.Value, items);
            await UpdateUnlockedItem(request.UnlockedTorsoStyle!.Value, items);
            await UpdateUnlockedItem(request.UnlockedLegStyle!.Value, items);
            await UpdateUnlockedItem(request.UnlockedBagStyle!.Value, items);
            await UpdateUnlockedItem(request.UnlockedAcessoryStyle!.Value, items);

            _mapper.Map(request, user.Preference);
            user.UpdatedAt = DateTime.UtcNow;

            _userUpdateOnlyRepository.Update(user);

            await _unitOfWork.Commit();
        }

        public void Validate(RequestUpdateUnlockedStylesUserJson request)
        {
            var validator = new UnlockedStylesValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorsMessages);
            }
        }

        private async Task UpdateUnlockedItem(Guid styleId, List<Item> items)
        {
            var item = items.FirstOrDefault(i => i.StyleId == styleId);

            if (item != null)
            {
                item.Unlocked = true;

                _itemUpdateOnlyRepository.Update(item);

                await _unitOfWork.Commit();
            }
        }
    }
}
