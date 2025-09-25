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
                .ForMember(dest => dest.Preferences, opt => opt.MapFrom(src => new ResponsePreferencesJson
                {
                    MapStyle = src.Preference.MapStyle,
                    AvatarOverProfile = src.Preference.AvatarOverProfile
                }))
                .ForMember(dest => dest.UnlockedStyles, opt => opt.MapFrom(src => new ResponseUnlockedStylesJson
                {
                    UnlockedMapStyles = src.Preference.UnlockedMapStyles
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
                        Accessories = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Accessories)!.StyleId,
                        Top = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Top)!.StyleId,
                        FacialHair = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.FacialHair)!.StyleId,
                        Clothes = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Clothes)!.StyleId,
                        Eyes = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Eyes)!.StyleId,
                        Eyebrown = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Eyebrown)!.StyleId,
                        Mouth = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Mouth)!.StyleId,
                        Skin = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Skin)!.StyleId,
                        BackPack = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.BackPack)!.StyleId
                    }
                }))
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
                    Views = src.Views,
                    Likes = src.UserPointOfInterestRelations.Count(),
                    Commentaries = src.Comments.Count()
                }));

            CreateMap<User, ResponseAuthor>()
                .ForMember(dest => dest.AvatarStyle, opt => opt.MapFrom(src => new ResponseAvatarStyle
                {
                    Accessories = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Accessories)!.StyleId,
                    Top = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Top)!.StyleId,
                    FacialHair = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.FacialHair)!.StyleId,
                    Clothes = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Clothes)!.StyleId,
                    Eyes = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Eyes)!.StyleId,
                    Eyebrown = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Eyebrown)!.StyleId,
                    Mouth = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Mouth)!.StyleId,
                    Skin = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.Skin)!.StyleId,
                    BackPack = src.Preference.Items.FirstOrDefault(i => i.Equipped && i.Style!.StyleType == StyleType.BackPack)!.StyleId
                }));

            CreateMap<Address, ResponseCoordinates>();

            CreateMap<Address, ResponseAddress>();

            CreateMap<PointOfInterest, ResponsePaginationPointOfInterestJson>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => new ResponsePaginationAuthor
                {
                    Name = src.User!.Name,
                    AvatarStyle = new ResponsePaginationAvatarStyle
                    {
                        Accessories = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Accessories)!.StyleId,
                        Top = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Top)!.StyleId,
                        FacialHair = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.FacialHair)!.StyleId,
                        Clothes = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Clothes)!.StyleId,
                        Eyes = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Eyes)!.StyleId,
                        Eyebrown = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Eyebrown)!.StyleId,
                        Mouth = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Mouth)!.StyleId,
                        Skin = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.Skin)!.StyleId,
                        BackPack = src.User!.Preference.Items.FirstOrDefault(i => i.Style!.StyleType == StyleType.BackPack)!.StyleId
                    }
                }))
                .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => new ResponsePaginationCoordinates
                {
                    Latitude = src.Address!.Latitude,
                    Longitude = src.Address!.Longitude
                }))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new ResponsePaginationAddress
                {
                    Country = src.Address!.Country,
                    State = src.Address.State,
                    City = src.Address.City,
                    District = src.Address.District,
                    Street = src.Address.Street,
                    Cep = src.Address.Cep,
                    Number = src.Address.Number
                }))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new ResponsePaginationStatus
                {
                    Views = src.Views,
                    Likes = src.UserPointOfInterestRelations.Count(),
                    Commentaries = src.Comments.Count()
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
