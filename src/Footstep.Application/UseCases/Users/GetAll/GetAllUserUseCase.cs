using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;

namespace Footstep.Application.UseCases.Users.GetAll
{
    public class GetAllUserUseCase : IGetAllUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetAllUserUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<ResponseUserJson>> Execute()
        {
            var users = await _userReadOnlyRepository.GetAll();

            List<ResponseUserJson> responses = new List<ResponseUserJson>();

            foreach (var user in users)
            {
                var items = await _itemReadOnlyRepository.GetByPreferenceIdAndUnlocked(user.PreferenceId);

                ResponseUserJson response = _mapper.Map<ResponseUserJson>(user);

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

                responses.Add(response);
            }

            return responses;
        }
    }
}
