using AutoMapper;
using ShahiiERP.Application.Features.Auth.Login;
using ShahiiERP.Domain.Entities.Identity;

namespace ShahiiERP.Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, LoginResponse>()
            .ForMember(dest => dest.DisplayName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}
