using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
using NotesApp.Contracts.NoteContracts;
using NotesApp.Enums;
using NotesApp.Extensions;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class NotesController(INoteRepository noteRepository) : BaseController
{

    [HttpGet]
    public ActionResult<ListOfNotes> GetAllNotes(bool? isCompleted, PriorityOfExecution? priorityOfExecution, 
        string? title, SortNotesBy? sortNotesBy)
    {
        var userId = GetUserId();
        var notes = noteRepository.GetAllNote(userId, isCompleted, priorityOfExecution, title, sortNotesBy);
        return Ok(notes);
    }

    [HttpPost]
    public ActionResult<NoteVm> CreateNote([FromBody] CreateNoteDto dto)
    {
        var userId = GetUserId();
        var note = noteRepository.CreateNote(dto, userId!.Value);
        return Ok(note);
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
        var userId = GetUserId();
        noteRepository.DeleteNote(id, userId);
        return NoContent();
    }

    private Guid? GetUserId()
    {
        var userId = HttpContext.ExtractUserIdFromClaims();
        if (userId is null)
            throw new UnauthorizedAccessException();
        return userId;
    }
}