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
            
            CreateMap<RequestPointOfInterestJson, PointOfInterest>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.Address, opt => opt.Ignore());
            
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
                    Map = "",
                    POI = "",
                    AvatarOverProfile = src.Preference.AvatarOverProfile,
                    Avatar = new ResponseUserCharacterStyleJson
                    {
                        Skin = "",
                        Top = new ResponseUserItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Backpack = new ResponseUserItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Clothe = new ResponseUserItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Eye = "",
                        Eyebrow = "",
                        Mouth = "",
                        FacialHair = new ResponseUserItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Accessory = new ResponseUserItemJson
                        {
                            Style = "",
                            Color = ""
                        }
                    }
                }))
                .ForMember(dest => dest.UnlockedStyles, opt => opt.MapFrom(src => new ResponseUserUnlockedStyles
                {
                    Map = new List<string> { "" },
                    POI = new List<string> { "" },
                    Avatar = new ResponseUserCharacterStylesJson
                    {
                        Skin = new List<string> { "" },
                        Top = new List<ResponseUserItemJson>
                        {
                            new ResponseUserItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        },
                        Backpack = new List<ResponseUserItemJson>
                        {
                            new ResponseUserItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        },
                        Clothe = new List<ResponseUserItemJson>
                        {
                            new ResponseUserItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        },
                        Eye = new List<string> { "" },
                        Eyebrow = new List<string> { "" },
                        Mouth = new List<string> { "" },
                        FacialHair = new List<ResponseUserItemJson>
                        {
                            new ResponseUserItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        },
                        Accessory = new List<ResponseUserItemJson>
                        {
                            new ResponseUserItemJson
                            {
                                Style = "",
                                Color = ""
                            }
                        }
                    }
                }));

            CreateMap<User, ResponsePaginationUserJson>()
                 .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => new ResponsePaginationUserProfilePictureJson
                 {
                     Uri = "",
                     Style = ""
                 }))
                .ForMember(dest => dest.Social, opt => opt.MapFrom(src => new ResponsePaginationUserSocialJson
                {
                    Followers = src.Followers.Select(f => f.FollowerId).ToList(),
                    Following = src.Following.Select(f => f.FollowingId).ToList(),
                }))
                .ForMember(dest => dest.Preferences, opt => opt.MapFrom(src => new ResponsePaginationUserPreferencesJson
                {
                    AvatarOverProfile = src.Preference.AvatarOverProfile,
                    Avatar = new ResponsePaginationUserCharacterStyleJson
                    {
                        Skin = "",
                        Top = new ResponsePaginationUserItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Backpack = new ResponsePaginationUserItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Clothe = new ResponsePaginationUserItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Eye = "",
                        Eyebrow = "",
                        Mouth = "",
                        FacialHair = new ResponsePaginationUserItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Accessory = new ResponsePaginationUserItemJson
                        {
                            Style = "",
                            Color = ""
                        }
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
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => new ResponseAuthor
                {
                    Name = src.User!.Name,
                    AvatarStyle = new ResponseAvatarStyle
                    {
                        Head = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Head)!.StyleId,
                        Body = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Body)!.StyleId,
                        Leg = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Leg)!.StyleId,
                        Bag = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Bag)!.StyleId,
                        Accessory = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Accessory)!.StyleId
                    }
                }))
                .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => new ResponseCoordinates
                {
                    Latitude = src.Address!.Latitude,
                    Longitude = src.Address!.Longitude
                }))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => new List<string> { "" }))
                .ForMember(dest => dest.Media, opt => opt.MapFrom(src => new ResponsePointOfInterestMediaJson
                {
                    Images = new List<string> { "" },
                    Videos = new List<string> { "" }
                }))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new ResponsePointOfInterestStatusJson
                {
                    Views = src.Views,
                    Likes = src.UserPointOfInterestRelations.Where(upoir => upoir.Like).Count(),
                    Comments = src.Comments.Count()
                }))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new ResponsePointOfInterestAddressJson
                {
                    Head = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Head)!.StyleId,
                    Body = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Body)!.StyleId,
                    Leg = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Leg)!.StyleId,
                    Bag = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Bag)!.StyleId,
                    Accessory = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Accessory)!.StyleId,
                }));

            CreateMap<PointOfInterest, ResponsePaginationPointOfInterestJson>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => new ResponsePaginationPointOfInterestAuthorJson
                {
                    Id = src.User!.Id,
                    Name = src.User!.Name,
                    AvatarOverProfile = src.User!.Preference.AvatarOverProfile,
                    Avatar = new ResponsePaginationPointOfInterestCharacterStyleJson
                    {
                        Skin = "",
                        Top = new ResponsePaginationPointOfInterestItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Backpack = new ResponsePaginationPointOfInterestItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Clothe = new ResponsePaginationPointOfInterestItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Eye = "",
                        Eyebrow = "",
                        Mouth = "",
                        FacialHair = new ResponsePaginationPointOfInterestItemJson
                        {
                            Style = "",
                            Color = ""
                        },
                        Accessory = new ResponsePaginationPointOfInterestItemJson
                        {
                            Style = "",
                            Color = ""
                        }
                    },
                    ProfilePicture = new ResponsePaginationPointOfInterestProfilePictureJson
                    {
                        Head = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Head)!.StyleId,
                        Body = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Body)!.StyleId,
                        Leg = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Leg)!.StyleId,
                        Bag = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Bag)!.StyleId,
                        Accessory = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Accessory)!.StyleId
                    }
                }))
                .ForMember(dest => dest.POIType, opt => opt.MapFrom(src => src.PointOfInterestType))
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => new ResponsePaginationPointOfInterestCoordinatesJson
                {
                    Latitude = src.Address!.Latitude,
                    Longitude = src.Address!.Longitude
                }))
                .ForMember(dest => dest.Media, opt => opt.MapFrom(src => new ResponsePaginationPointOfInterestMediaJson
                {
                    Image = ""
                }))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new ResponsePaginationPointOfInterestStatusJson
                {
                    Views = src.Views,
                    Likes = src.UserPointOfInterestRelations.Where(upoir => upoir.Like).Count(),
                    Comments = src.Comments.Count()
                }))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new ResponsePaginationPointOfInterestAddressJson
                {
                    Country = src.Address!.Country,
                    State = src.Address!.State,
                    City = src.Address!.City,
                    District = src.Address!.District,
                    Street = src.Address!.Street,
                    Cep = src.Address!.Cep,
                    Number = src.Address!.Number
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
