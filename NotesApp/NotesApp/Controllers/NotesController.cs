using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
using NotesApp.Contracts;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class NotesController : ControllerBase
{
    private readonly INoteRepository _noteRepository;

    public NotesController(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    [HttpGet]
    public ActionResult<ListOfNotes> GetAllNotes()
        => Ok(_noteRepository.GetAllNote());

    [HttpPost]
    public ActionResult<NoteVm> CreateNote(CreateNoteDto dto)
    {
        _noteRepository.CreateNote(dto);
        return Ok("Note created successfully");
    }

    [HttpGet("title")]
    public ActionResult<List<NoteTitleViewModel>> GetNoteTitle()
    {
        var notes = _noteRepository.GetAllNote().Notes
            .Select(n => new NoteTitleViewModel(
                n.Title))
            .ToList();;
        return Ok(notes);
    }

    [HttpGet("description")]
    public ActionResult<List<NoteDescriptionViewModel>> GetNoteDescription()
    {
        var notes = _noteRepository.GetAllNote().Notes
            .Select(n => new NoteDescriptionViewModel(
                n.Description))
            .ToList();
        return Ok(notes);
    }

    [HttpPut]
    public ActionResult<bool> UpdateNoteDto(UpdateNoteDto dto)
    {
        var result = _noteRepository.UpdateNote(dto);
        if (!result)
        {
            return BadRequest("Failed to update note");
        }
        return Ok("Note changed!");
    }

    [HttpDelete]
    public ActionResult<bool> DeleteNote(int noteId)
    {
        _noteRepository.DeleteNote(noteId);

        if (true)
        {
            return Ok("Note deleted");
        }
    }
}