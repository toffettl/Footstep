using AutoMapper;
using Footstep.Communication.Enums;
using Footstep.Communication.Requests.Users;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Users.UpdatePreferences
{
    public class UpdatePreferencesUserUseCase : IUpdatePreferencesUserUseCase
    {
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IItemReadOnlyRepository _itemReadOnlyRepository;
        private readonly IItemUpdateOnlyRepository _itemUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePreferencesUserUseCase(
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

        public async Task Execute(Guid Id, RequestUpdatePreferencesUserJson request)
        {
            Validate(request);

            var user = await _userUpdateOnlyRepository.GetById(Id);

            if (user == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            var items = await _itemReadOnlyRepository.GetByPreferenceIdAndUnlocked(user!.PreferenceId);

            UpdateEquippedItem(user.PreferenceId, request.PointOfInterestStyle!.Value, StyleType.PointOfInterest, items);
            UpdateEquippedItem(user.PreferenceId, request.AvatarStyle!.Head!.Value, StyleType.Accessories, items);
            UpdateEquippedItem(user.PreferenceId, request.AvatarStyle!.Body!.Value, StyleType.Top, items);
            UpdateEquippedItem(user.PreferenceId, request.AvatarStyle!.Leg!.Value, StyleType.FacialHair, items);
            UpdateEquippedItem(user.PreferenceId, request.AvatarStyle!.Bag!.Value, StyleType.Eyes, items);
            UpdateEquippedItem(user.PreferenceId, request.AvatarStyle!.Acessory!.Value, StyleType.Eyebrown, items);

            _mapper.Map(request, user.Preference);

            user.UpdatedAt = DateTime.UtcNow;

            _userUpdateOnlyRepository.Update(user);

            await _unitOfWork.Commit();
        }

        public void Validate(RequestUpdatePreferencesUserJson request)
        {
            var validator = new PreferencesValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorsMessages);
            }
        }

        private void UpdateEquippedItem(Guid preferenceId, Guid styleId, StyleType styleType, List<Item> items)
        {
            var pastEquippedItem = items.FirstOrDefault(i => i.Style?.StyleType == (Domain.Enums.StyleType)(int)styleType && i.Equipped);
            var newEquippedItem = items.FirstOrDefault(i => i.PreferenceId == preferenceId && i.StyleId == styleId);

            if (newEquippedItem == null)
            {
                throw new NotFoundException(ResourceErrorMessages.ITEM_NOT_FOUND);
            }

            pastEquippedItem!.Equipped = false;
            newEquippedItem!.Equipped = true;

            _itemUpdateOnlyRepository.Update(pastEquippedItem);
            _itemUpdateOnlyRepository.Update(newEquippedItem);
        }
    }
}
