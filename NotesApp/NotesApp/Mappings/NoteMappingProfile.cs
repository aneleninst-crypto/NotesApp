using AutoMapper;
using NotesApp.Contracts.NoteContracts;
using NotesApp.Models;

namespace NotesApp.Mappings;

public class NoteMappingProfile : Profile
{
    public NoteMappingProfile()
    {
        CreateMap<Note, NoteVm>();
        CreateMap<Note, NoteListVm>();
        CreateMap<IEnumerable<Note>, ListOfNotes>()
            .ForCtorParam(nameof(ListOfNotes.Notes), 
                opt => opt.MapFrom(
                    src => src));
        CreateMap<CreateNoteDto, Note>()
            .ForMember(dest => dest.Id, 
                opt => opt.Ignore());
        CreateMap<UpdateNoteDto, Note>()
            .ForAllMembers(opt => opt.Condition(
                (src, dest, srcMember
                ) => srcMember != null));
    }
}