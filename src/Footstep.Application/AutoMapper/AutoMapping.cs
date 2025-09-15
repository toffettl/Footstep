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
using Footstep.Domain.Enums;

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
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RequestUpdateUnlockedStylesUserJson, Preference>();

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
            CreateMap<User, ResponseUserJson>()
                .ForMember(dest => dest.Preferences, opt => opt.MapFrom(src => new ResponsePreferences
                {
                    MapStyle = src.Preference.MapStyle,
                    PointOfInterestStyle = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.PointOfInterest)!.Style!.Image,
                    AvatarOverProfile = src.Preference.AvatarOverProfile,
                    AvatarStyle = new ResponseAvatarStyleJson()
                    {
                        Head = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Head && i.Equipped)!.Style!.Image,
                        Body = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Torso && i.Equipped)!.Style!.Image,
                        Leg = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Leg && i.Equipped)!.Style!.Image,
                        Bag = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Bag && i.Equipped)!.Style!.Image,
                        Accessory = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Head && i.Equipped)!.Style!.Image
                    }
                }))
                .ForMember(dest => dest.Preferences, opt => opt.MapFrom(src => new ResponseUnlockedStyles
                {
                    UnlockedMapStyles = src.Preference.MapStyle,
                    UnlockedPointOfInterestStyles = src.Preference.Items.Where(i => i.Style!.StyleType == StyleType.PointOfInterest).Select(i => i.Style!.Image).ToList()!,
                    UnlockedHeadStyles = src.Preference.Items.Where(i => i.Style!.StyleType == StyleType.Head).Select(i => i.Style!.Image).ToList()!,
                    UnlockedTorsoStyles = src.Preference.Items.Where(i => i.Style!.StyleType == StyleType.Torso).Select(i => i.Style!.Image).ToList()!,
                    UnlockedLegStyles = src.Preference.Items.Where(i => i.Style!.StyleType == StyleType.Leg).Select(i => i.Style!.Image).ToList()!,
                    UnlockedBagStyles = src.Preference.Items.Where(i => i.Style!.StyleType == StyleType.Bag).Select(i => i.Style!.Image).ToList()!,
                    UnlockedAcessoryStyles = src.Preference.Items.Where(i => i.Style!.StyleType == StyleType.Accessory).Select(i => i.Style!.Image).ToList()!
                }));

            CreateMap<Comment, ResponseCommentJson>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new StatusResponse
                    {
                    Likes = src.CommentLikes.Count,
                    Replies = src.Comments.Count,
                }));

            CreateMap<PointOfInterest, ResponsePointOfInterestJson>()
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
