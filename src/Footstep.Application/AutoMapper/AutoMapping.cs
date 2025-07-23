using AutoMapper;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses.Comments;
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
            CreateMap<RequestPointOfInterestJson, PointOfInterest>();
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, config => config.Ignore());
            CreateMap<RequestUpdatePreferencesUserJson, User>()
                .ForMember(dest => dest.HeadStyle, config => config.MapFrom(src => src.AvatarStyle!.Head))
                .ForMember(dest => dest.TorsoStyle, config => config.MapFrom(src => src.AvatarStyle!.Torso))
                .ForMember(dest => dest.LegStyle, config => config.MapFrom(src => src.AvatarStyle!.Leg))
                .ForMember(dest => dest.BagStyle, config => config.MapFrom(src => src.AvatarStyle!.Bag))
                .ForMember(dest => dest.AcessoryStyle, config => config.MapFrom(src => src.AvatarStyle!.Acessory));
            CreateMap<RequestUpdateUnlockedStylesUserJson, User>();
            CreateMap<RequestCommentJson, Comment>();
            CreateMap<RequestUpdateStatusCommentsJson, Comment>();
        }

        private void EntityToResponse()
        {
            CreateMap<User, ResponseGetUserJson>()
                .ForMember(dest => dest.Preferences, config => config.MapFrom(src => new ResponsePreferencesJson
                {
                    MapStyle = src.MapStyle,
                    PointOfInterestStyle = src.PointOfInterestStyle,
                    AvatarOverProfile = src.AvatarOverProfile,
                    AvatarStyle = new ResponseAvatarStyleJson
                    {
                        Head = src.HeadStyle,
                        Torso = src.TorsoStyle,
                        Leg = src.LegStyle,
                        Bag = src.BagStyle,
                        Acessory = src.AcessoryStyle,
                    }
                }))
                .ForMember(dest => dest.UnlockedStyles, config => config.MapFrom(src => new ResponseUnlockedStylesJson
                {
                    UnlockedMapStyles = src.UnlockedMapStyles,
                    UnlockedPointOfInterestStyles = src.UnlockedPointOfInterestStyles,
                    UnlockedHeadStyles = src.UnlockedHeadStyles,
                    UnlockedTorsoStyles = src.UnlockedTorsoStyles,
                    UnlockedLegStyles = src.UnlockedLegStyles,
                    UnlockedBagStyles = src.UnlockedBagStyles,
                    UnlockedAcessoryStyles = src.UnlockedAcessoryStyles
                }));
            CreateMap<PointOfInterest, ResponseCreatePointOfInterestJson>();
            CreateMap<PointOfInterest, ResponsePointOfIntereseJson>();
            CreateMap<Comment, ResponseCommentJson>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new StatusResponse
                    {
                        Likes = src.Likes,
                        Replies = src.Replies,
                    }));
        }
    }
}
