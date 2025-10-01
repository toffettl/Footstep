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

namespace Footstep.Application.UseCases.AutoMapper
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

            CreateMap<RequestUserRelationJson, Followership>();
            
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
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => new ResponseUserProfilePictureJson
                {
                    Uri = "",
                    Style = ""
                }))
                .ForMember(dest => dest.Social, opt => opt.MapFrom(src => new ResponseUserSocialJson
                {
                    Followers = src.Followers.Select(f => f.FollowerId).ToList(),
                    Following = src.Following.Select(f => f.FollowingId).ToList(),
                    Comments = src.Comments.Select(c => c.Id).ToList()
                }))
                .ForMember(dest => dest.Activity, opt => opt.MapFrom(src => new ResponseUserActivityJson
                {
                    POIs = new ResponseUserPOIsJson
                    {
                        Steps = src.PointsOfInterest.Where(p => p.PointOfInterestType == PointOfInterestType.Step).Select(p => p.Id).ToList(),
                        Marks = src.PointsOfInterest.Where(p => p.PointOfInterestType == PointOfInterestType.Mark).Select(p => p.Id).ToList()
                    },
                    Coins = new ResponseUserCoinsJson
                    {
                        Total = src.Coin.Total,
                        Spent = src.Coin.Spent,
                        Earned = src.Coin.Earned
                    }
                }))
                .ForMember(dest => dest.Preferences, opt => opt.MapFrom(src => new ResponseUserPreferencesJson
                {
                    Map = src.Preference.MapStyle,
                    POI = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.PointOfInterest)!.Style!.Image,
                    AvatarOverProfile = src.Preference.AvatarOverProfile,
                    Avatar = new ResponseUserCharacterStyleJson
                    {
                        Skin = "",
                        Top = new ResponseItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Backpack = new ResponseItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Clothe = new ResponseItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Eye = "",
                        Eyebrow = "",
                        Mouth = "",
                        FacialHair = new ResponseItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Accessory = new ResponseItemJson
                        {
                            Style = "",
                            Color = ""
                        }
                    }
                }))
                .ForMember(dest => dest.UnlockedStyles, opt => opt.MapFrom(src => new ResponseUserUnlockedStyles
                {
                    Map = new List<string> { src.Preference.MapStyle! },
                    POI = new List<string> { src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.PointOfInterest)!.Style!.Image! },
                    Avatar = new ResponseUserCharacterStylesJson
                    {
                        Skin = new List<string> { "" },
                        Top = new List<ResponseItemJson>
                        {
                            new ResponseItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        },
                        Backpack = new List<ResponseItemJson>
                        {
                            new ResponseItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        },
                        Clothe = new List<ResponseItemJson>
                        {
                            new ResponseItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        },
                        Eye = new List<string> { "" },
                        Eyebrow = new List<string> { "" },
                        Mouth = new List<string> { "" },
                        FacialHair = new List<ResponseItemJson>
                        {
                            new ResponseItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        },
                        Accessory = new List<ResponseItemJson>
                        {
                            new ResponseItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        }
                    }
                }));

            CreateMap<User, ResponsePaginationUserJson>()
                 .ForMember(dest => dest.Preferences, opt => opt.MapFrom(src => new ResponsePaginationPreferences
                 {
                     AvatarOverProfile = src.Preference.AvatarOverProfile,
                     AvatarStyle = new ResponsePaginationAvatarStyle()
                {
                         Head = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Head && i.Equipped)!.Style!.Image,
                         Body = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Torso && i.Equipped)!.Style!.Image,
                         Leg = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Leg && i.Equipped)!.Style!.Image,
                         Bag = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Bag && i.Equipped)!.Style!.Image,
                         Accessory = src.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Head && i.Equipped)!.Style!.Image
                     }
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
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new ResponseAddress
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

            CreateMap<Followership, ResponseFollowersJson>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Follower!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Follower!.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Follower!.Email));

            CreateMap<Followership, ResponseFollowingJson>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Following!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Following!.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Following!.Email));

            CreateMap<Followership, ResponseUserRelationJson>();

            CreateMap<Style, ResponseStyleJson>();
        }
    }
}
