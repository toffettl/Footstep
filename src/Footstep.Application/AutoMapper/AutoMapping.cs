﻿using AutoMapper;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Requests.Users;
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
        }

        private void EntityToResponse()
        {
            CreateMap<Trace, ResponseCreateTraceJson>();
            CreateMap<Trace, ResponseTraceJson>();
        }
    }
}
