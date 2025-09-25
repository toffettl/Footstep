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

            response.Preferences!.AvatarStyle!.Head = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Head && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Body = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Body && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Leg = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Leg && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Bag = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Bag && i.Equipped)?.Style?.Id;
            response.Preferences!.AvatarStyle!.Acessory = items.FirstOrDefault(i => i.Style?.StyleType == StyleType.Accessory && i.Equipped)?.Style?.Id;

            response.UnlockedStyles!.UnlockedMapStyles = user.Preference.MapStyle;
            response.UnlockedStyles!.UnlockedPointOfInterestStyles = items.Where(i => i.Style?.StyleType == StyleType.PointOfInterest).Select(i => i.StyleId).ToList();
            response.UnlockedStyles!.UnlockedHeadStyles = items.Where(i => i.Style?.StyleType == StyleType.Head).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedBodyStyles = items.Where(i => i.Style?.StyleType == StyleType.Body).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedLegStyles = items.Where(i => i.Style?.StyleType == StyleType.Leg).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedBagStyles = items.Where(i => i.Style?.StyleType == StyleType.Bag).Select(i => i.Style!.Id).ToList();
            response.UnlockedStyles!.UnlockedAcessoryStyles = items.Where(i => i.Style?.StyleType == StyleType.Accessory).Select(i => i.Style!.Id).ToList();

            return response ;
        }
    }
}
