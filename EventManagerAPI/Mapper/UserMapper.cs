using AutoMapper;
using EventManagerAPI.DTO.Request;
using EventManagerAPI.DTO.Response;
using EventManagerAPI.Models;

namespace EventManagerAPI.Mapper
{
	public class UserMapper : Profile
	{
		public UserMapper()
		{
			CreateMap<AppUser, UserResponse>().ReverseMap();

			CreateMap<UserCreationRequest, AppUser>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

			CreateMap<UserUpdateRequest, AppUser>();

		}
	}
}
