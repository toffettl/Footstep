using AutoMapper;
using Footstep.Communication.Requests;
using Footstep.Communication.Responses;
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
        }

        private void EntityToResponse()
        {
            CreateMap<Trace, ResponseCreateTraceJson>();
        }
    }
}
