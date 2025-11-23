using AutoMapper;
using NotesApp.Contracts;
using NotesApp.Models;

namespace NotesApp.Configurations.Mappings;

public class NoteMappingProfile : Profile
{
    public NoteMappingProfile()
    {
        CreateMap<Note, NoteVm>();
        CreateMap<Note, NoteListVm>();
        CreateMap<Note, NoteTitleViewModel>();
        CreateMap<Note, NoteDescriptionViewModel>();
        CreateMap<IEnumerable<Note>, ListOfNotes>()
            .ForCtorParam(nameof(ListOfNotes.Notes),
                opt => opt.MapFrom(notes =>
                    notes.Select(n => new NoteListVm(
                        n.Title, 
                        n.Description, 
                        n.Priority
                        )).ToList()
                    )
                );
        CreateMap<CreateNoteDto, Note>()
            .ForMember(dest => dest.Id, 
                opt => opt.Ignore());
        CreateMap<UpdateNoteDto, Note>()
            .ForAllMembers(opt => opt.Condition(
                (src, dest, srcMember
                ) => srcMember != null));
    }
}