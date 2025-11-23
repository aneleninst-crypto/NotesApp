using AutoMapper;
using NotesApp.Contracts;
using NotesApp.Models;


namespace NotesApp.Configurations.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserVm>();
        CreateMap<User, UserListVm>();
        CreateMap<IEnumerable<User>, ListOfUsers>()
            .ForCtorParam(nameof(ListOfUsers.Users),
            source => 
                source.MapFrom(
                    userList => userList
                    .ToList()));
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());
    }
    
}