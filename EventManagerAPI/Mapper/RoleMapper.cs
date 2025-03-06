using AutoMapper;
using EventManagerAPI.DTO.Response;
using Microsoft.AspNetCore.Identity;

namespace EventManagerAPI.Mapper
{
	public class RoleMapper : Profile
	{
		public RoleMapper() {
			CreateMap<IdentityRole, RoleResponse>().ReverseMap();
		}
	}
}
