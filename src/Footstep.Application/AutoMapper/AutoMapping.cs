using AutoMapper;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Requests.Styles;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Requests.UserRelation;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses.Comments;
using Footstep.Communication.Responses.Styles;
using Footstep.Communication.Responses.Traces;
using Footstep.Communication.Responses.UserRelation;
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
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, config => config.Ignore());
            
            CreateMap<RequestUserRelationJson, UserRelation>();
            
            CreateMap<RequestStyleJson, Style>();
            
            CreateMap<RequestPointOfInterestJson, PointOfInterest>();
            
            CreateMap<RequestPointOfInterestJson, PointOfInterest>();
            
            CreateMap<RequestUpdateStatusPointOfInterestJson, PointOfInterest>();

            CreateMap<RequestUpdatePointOfInterestJson, PointOfInterest>();

            CreateMap<RequestCommentJson, Comment>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AuthorId));
            
            CreateMap<RequestUpdateContentComment, Comment>();
        }

        private void EntityToResponse()
        {
            CreateMap<User, ResponseGetUserJson>()
                .ForMember(dest => dest.Preferences, config => config.MapFrom(src => new ResponsePreferencesJson
                {
                    //    MapStyle = src.MapStyle,
                    //    PointOfInterestStyle = src.PointOfInterestStyle,
                    //    AvatarOverProfile = src.AvatarOverProfile,
                    //    AvatarStyle = new ResponseAvatarStyleJson
                    //    {
                    //        Head = src.HeadStyle,
                    //        Torso = src.TorsoStyle,
                    //        Leg = src.LegStyle,
                    //        Bag = src.BagStyle,
                    //        Acessory = src.AcessoryStyle,
                    //    }
                }))
                .ForMember(dest => dest.UnlockedStyles, config => config.MapFrom(src => new ResponseUnlockedStylesJson
                    {
                    //UnlockedMapStyles = src.UnlockedMapStyles,
                    //UnlockedPointOfInterestStyles = src.UnlockedPointOfInterestStyles,
                    //UnlockedHeadStyles = src.UnlockedHeadStyles,
                    //UnlockedTorsoStyles = src.UnlockedTorsoStyles,
                    //UnlockedLegStyles = src.UnlockedLegStyles,
                    //UnlockedBagStyles = src.UnlockedBagStyles,
                    //UnlockedAcessoryStyles = src.UnlockedAcessoryStyles
                }));

            CreateMap<User, ResponseUsersJson>();

            CreateMap<Comment, ResponseCommentJson>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new StatusResponse
                    {
                    Likes = src.CommentLikes.Count,
                    Replies = src.Comments.Count,
                }));

            CreateMap<PointOfInterest, ResponsePointOfIntereseJson>()
                .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => new ResponseCoordinates
                {
                    Latitude = src.Address!.Latitude,
                    Longitude = src.Address!.Longitude
                }))
                .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => new ResponseAdress
                {
                    Country = src.Address!.Country,
                    State = src.Address.State,
                    City = src.Address.City,
                    District = src.Address.District,
                    Street = src.Address.Street,
                    Cep = src.Address.Cep,
                    Number = src.Address.Number
                }))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new ResponseStatus
                {
                    //Views = src.Views,
                    //Likes = src.Likes
                }));

            CreateMap<UserRelation, ResponseFollowersJson>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Follower!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Follower!.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Follower!.Email));

            CreateMap<UserRelation, ResponseFollowingJson>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Following!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Following!.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Following!.Email));

            CreateMap<UserRelation, ResponseUserRelationJson>();

            CreateMap<Style, ResponseStyleJson>();
        }
    }
}
