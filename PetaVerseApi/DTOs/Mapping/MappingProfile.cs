using AutoMapper;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs.Create;

namespace PetaVerseApi.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PetaverseMedia, PetaverseMediaDTO>();
                //.ForMember(p => p.Type, opt => opt.MapFrom(p => p.Type.ToString()));
            CreateMap<PetaverseMediaDTO, PetaverseMedia>()
                .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<Breed, BreedDTO>();
            //CreateMap<Breed, int>().ConvertUsing(b => b.Id);
            CreateMap<BreedDTO, Breed>()
                .ForMember(b => b.Id, opt => opt.Ignore());

            CreateMap<Species, SpeciesDTO>();
            CreateMap<SpeciesDTO, Species>()
                .ForMember(s => s.Id, opt => opt.Ignore());

            CreateMap<PetShorts, PetShortsDTO>();
            CreateMap<PetShortsDTO, PetShorts>()
                .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<Animal, AnimalDTO>();
            CreateMap<AnimalDTO, Animal>()
                .ForMember(a => a.Id, opt => opt.Ignore());

            CreateMap<CreatePetDTO, Animal>();

            CreateMap<Animal, AnimalDetailDTO>();

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
