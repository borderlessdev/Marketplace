using AutoMapper;
using Marketplace.Communication.Request;
using Martkeplace.Domain.Entity;

namespace Marketplace.Application.Services.AutoMapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<RequestSignupUserJson, User>();
    }
}
