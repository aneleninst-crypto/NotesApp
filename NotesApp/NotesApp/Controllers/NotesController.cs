using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
using NotesApp.Contracts.NoteContracts;
using NotesApp.Extensions;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class NotesController(INoteRepository noteRepository) : BaseController
{

    [HttpGet]
    public ActionResult<ListOfNotes> GetAllNotes()
    {
        var notes = noteRepository.GetAllNote();
        return Ok(notes);
    }

    [HttpPost]
    public ActionResult<NoteVm> CreateNote([FromBody] CreateNoteDto dto)
    {
        var userId = HttpContext.ExtractUserIdFromClaims();
        if (userId is null)
            return Unauthorized();
        var note = noteRepository.CreateNote(dto, userId.Value);
        return Ok(note);
    }

    [HttpGet("title")]
    public ActionResult<List<NoteTitleViewModel>> GetNoteTitle()
    {
        var notes = noteRepository.GetAllNote().Notes
            .Select(n => new NoteTitleViewModel(
                n.Title))
            .ToList(); // не используешь автомаппинг почему-то тут, единообразие важно, если это не сильно заебно
        return Ok(notes);
    }

    [HttpGet("description")]
    public ActionResult<List<NoteDescriptionViewModel>> GetNoteDescription()
    {
        var notes = noteRepository.GetAllNote().Notes
            .Select(n => new NoteDescriptionViewModel(
                n.Description)) // не используешь автомаппинг почему-то тут, единообразие важно, если это не сильно заебно
            .ToList();
        return Ok(notes);
    }

    [HttpPut]
    public ActionResult<bool> UpdateNoteDto(int noteId, UpdateNoteDto dto)
    {
        var result = noteRepository.UpdateNote(noteId, dto);
        if (!result)
        {
            return BadRequest("Failed to update note");
        }
        return Ok("Note changed!");
    }
    
    [Authorize(Policy = "NotesOwner")]
    [HttpDelete("{id:int}")]
    public ActionResult<bool> DeleteNote(int id)
    {
        var userId = HttpContext.ExtractUserIdFromClaims();
        if (userId is null)
            return Unauthorized();
        noteRepository.DeleteNote(id, userId);
        return NoContent();
    }
}