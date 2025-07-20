using AutoMapper;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Requests.UserRelation;
using Footstep.Communication.Requests.Users;
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
            CreateMap<RequestTraceJson, Trace>();
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, config => config.Ignore());
            CreateMap<RequestUserRelationJson, UserRelation>();
        }

        private void EntityToResponse()
        {
            CreateMap<Trace, ResponseCreateTraceJson>();
            CreateMap<Trace, ResponseTraceJson>();
            CreateMap<UserRelation, ResponseUserRelationJson>();
            CreateMap<UserRelation, ResponseFollowersJson>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Follower!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Follower!.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Follower!.Email));

            CreateMap<UserRelation, ResponseFollowingJson>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Following!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Following!.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Following!.Email));
        }
    }
}
