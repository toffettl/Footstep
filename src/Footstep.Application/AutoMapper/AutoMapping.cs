using AutoMapper;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses.Traces;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Entities;

namespace Footstep.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestTraceJson, Trace>();
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, config => config.Ignore());
        }

        private void EntityToResponse()
        {
            CreateMap<Trace, ResponseCreateTraceJson>();
            CreateMap<Trace, ResponseTraceJson>();
            CreateMap<User, ResponseGetUserJson>()
                .ForMember(dest => dest.Preferences, config => config.MapFrom(src => new Preferences
                {
                    MapStyle = src.MapStyle,
                    PointOfInterestStyle = src.PointOfInterestStyle,
                    AvatarOverProfile = src.AvatarOverProfile,
                    AvatarStyle = new AvatarStyle
                    {
                        Head = src.HeadStyle,
                        Torso = src.TorsoStyle,
                        Leg = src.LegStyle,
                        Bag = src.BagStyle,
                        Acessory = src.AcessoryStyle,
                    }
                }))
                .ForMember(dest => dest.UnlockedStyles, config => config.MapFrom(src => new UnlockedStyles
                {
                    UnlockedMapStyles = src.UnlockedMapStyles,
                    UnlockedPointOfInterestStyles = src.UnlockedPointOfInterestStyles,
                    UnlockedHeadStyles = src.UnlockedHeadStyles,
                    UnlockedTorsoStyles = src.UnlockedTorsoStyles,
                    UnlockedLegStyles = src.UnlockedLegStyles,
                    UnlockedBagStyles = src.UnlockedBagStyles,
                    UnlockedAcessoryStyles = src.UnlockedAcessoryStyles
                }));
        }
    }
}
