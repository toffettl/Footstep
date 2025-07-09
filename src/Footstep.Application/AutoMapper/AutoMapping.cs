using AutoMapper;
using Footstep.Communication.Requests.Marks;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses.Marks;
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
            CreateMap<RequestMarkJson, Mark>();
        }

        private void EntityToResponse()
        {
            CreateMap<Trace, ResponseCreateTraceJson>();
            CreateMap<Trace, ResponseTraceJson>();
            CreateMap<Mark, ResponseMarkJson>();
        }
    }
}
