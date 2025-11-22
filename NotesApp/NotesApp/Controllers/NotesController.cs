using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
using NotesApp.enams;
using NotesApp.Exceptions;
using NotesApp.Models;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class NotesController : ControllerBase
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;

    public NotesController(INoteRepository noteRepository, IUserRepository userRepository)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public ActionResult<List<Note>> GetAllNotes() 
        => Ok(_noteRepository.GetAllNote());

    [HttpPost]
    public ActionResult CreateNote(string? title, string? description, int userId, PriorityOfExecution priority)
    {
        _userRepository.GetUserById(userId);

        _noteRepository.CreateNote(title, description, userId, priority);

        return Ok("Note created successfully");
    }

    [HttpPut]
    public ActionResult<bool> ChangeNote(int noteId, int userId,string? title = null, PriorityOfExecution? priority = null, 
        string? description = null)
    {
        _userRepository.GetUserById(userId);
        _noteRepository.ChangeNote(noteId, userId, title, priority, description);
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