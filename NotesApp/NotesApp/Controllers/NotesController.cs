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
    public async Task<ActionResult<ListOfNotes>> GetAllNotes(bool? isCompleted, PriorityOfExecution? priorityOfExecution, 
        string? title, SortNotesBy? sortNotesBy)
    {
        var userId = GetUserId();
        var notes = await noteRepository.GetAllNoteAsync(userId, isCompleted, priorityOfExecution, title, sortNotesBy);
        return Ok(notes);
    }

    [HttpPost]
    public async Task<ActionResult<NoteVm>> CreateNote([FromBody] CreateNoteDto dto)
    {
        var userId = GetUserId();
        var note = await noteRepository.CreateNoteAsync(dto, userId!.Value);
        return Ok(note);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateNoteDto(int noteId, UpdateNoteDto dto)
    {
        var result = await noteRepository.UpdateNoteAsync(noteId, dto);
        if (!result)
            return BadRequest("Failed to update note");
        
        return Ok("Note changed!");
    }
    
    [Authorize(Policy = "NotesOwner")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteNote(int id)
    {
        var userId = GetUserId();
        await noteRepository.DeleteNoteAsync(id, userId);
        return NoContent();
    }

    private Guid? GetUserId()
    {
        var userId = HttpContext.ExtractUserIdFromClaims();
        return userId ?? throw new UnauthorizedAccessException();
    }
}