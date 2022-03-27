using AutoMapper;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs.Create;

namespace PetaVerseApi.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Breed, BreedDTO>();
            CreateMap<BreedDTO, Breed>()
                .ForMember(b => b.Id, opt => opt.Ignore());

            CreateMap<Species, SpeciesDTO>();
            CreateMap<SpeciesDTO, Species>()
                .ForMember(s => s.Id, opt => opt.Ignore());

            CreateMap<User, UserDTO>()
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.UserRoles.Select(ur => ur.Role!.Name)));
            CreateMap<UserDTO, User>()
                .ForMember(d => d.Guid, opt => opt.Ignore());
            CreateMap<CreateUserDTO, User>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();
        }
    }
}
