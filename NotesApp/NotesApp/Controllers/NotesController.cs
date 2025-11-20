using Microsoft.AspNetCore.Mvc;
using NotesApp.enams;
using NotesApp.Exceptions;
using NotesApp.Models;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class NotesController : ControllerBase
{
    private static readonly List<Note> Notes = new();
    
    
    [HttpGet]
    public ActionResult<List<Note>> GetAllNotes() => Ok(Notes);

    [HttpPost]
    public IActionResult CreateNote(string? title, string? description, int userId, PriorityOfExecution priority)
    {
        var newNote = new Note(Notes.Count+1, title, description, userId, priority );
        Notes.Add(newNote);
        return Ok("Note created!");
    }

    [HttpPut]
    public ActionResult<bool> ChangeNote(int noteId, string? title = null, PriorityOfExecution? priority = null, 
        string? description = null)
    {
        var note = Notes.SingleOrDefault(n => n.Id == noteId);

        if (note == null)
        {
            return false;
        }

        if (title == null && description == null)
        {
            return false;
        }

        if (title is not null)
        {
            note.Title = title;
        }

        if (description is not null)
        {
            note.Description = description;
        }

        if (priority is not null)
        {
            note.Priority = priority.Value;
        }

        return Ok("Note changed!");
    }

    [HttpDelete]
    public ActionResult<bool> DeleteNote(int noteId)
    {
        var note = Notes.SingleOrDefault(n => n.Id == noteId);
        if (note == null)
        {
            return false;
        }
        Notes.Remove(note);
        return Ok("Note deleted!");
    }
}