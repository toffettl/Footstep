using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Enums;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Users.GetById
{
    public class GetByIdUserUseCase : IGetByIdUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IItemReadOnlyRepository _itemReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetByIdUserUseCase(
            IUserReadOnlyRepository userReadOnlyRepository, 
            IItemReadOnlyRepository itemReadOnlyRepository,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _itemReadOnlyRepository = itemReadOnlyRepository;
            _mapper = mapper;
        }
            
        public async Task<ResponseUserJson> Execute(Guid id)
        {
            var user = await _userReadOnlyRepository.GetById(id);

            if (user == null)
            {
                throw new DirectoryNotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            var response = _mapper.Map<ResponseUserJson>(user);

            var items = await _itemReadOnlyRepository.GetByPreferenceIdAndUnlocked(user.PreferenceId);

            response.Preferences!.PointOfInterestStyle = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.PointOfInterest && i.Equipped)?.Style?.Id;

            response.Preferences!.AvatarStyle!.Accessories = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Accessories && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Top = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Top && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.FacialHair = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.FacialHair && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Clothes = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Clothes && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Eyes = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Eyes && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Eyebrown = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Eyebrown && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Mouth = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Mouth && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Skin = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Skin && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.BackPack = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.BackPack && i.Equipped)?.Style?.Id;

            response.UnlockedStyles!.UnlockedMapStyles = user.Preference.MapStyle;
            response.UnlockedStyles!.UnlockedPointOfInterestStyles = items.Where(i => i.Style?.StyleType == StyleType.PointOfInterest).Select(i => i.StyleId).ToList();
            response.UnlockedStyles!.UnlockedAccessoriesStyles = items.Where(i => i.Style?.StyleType == StyleType.Accessories).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedTopStyles = items.Where(i => i.Style?.StyleType == StyleType.Top).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedFacialHairStyles = items.Where(i => i.Style?.StyleType == StyleType.FacialHair).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedClothesStyles = items.Where(i => i.Style?.StyleType == StyleType.Clothes).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedEyesStyles = items.Where(i => i.Style?.StyleType == StyleType.Eyes).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedEyebrownStyles = items.Where(i => i.Style?.StyleType == StyleType.Eyebrown).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedMouthStyles = items.Where(i => i.Style?.StyleType == StyleType.Mouth).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedSkinStyles = items.Where(i => i.Style?.StyleType == StyleType.Skin).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedBackPackStyles = items.Where(i => i.Style?.StyleType == StyleType.BackPack).Select(i => i.Style!.Id).ToList();


            return response ;
        }
    }
}
