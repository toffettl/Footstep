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
            CreateMap<RequestCommentJson, Comment>();
            CreateMap<RequestUpdateStatusCommentsJson, Comment>();
        }

        private void EntityToResponse()
        {
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
