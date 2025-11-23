using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
using NotesApp.Contracts;
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
    public ActionResult<ListOfNotes> GetAllNotes()
        => Ok(_noteRepository.GetAllNote());

    [HttpPost]
    public ActionResult<NoteVm> CreateNote(CreateNoteDto dto)
    {
        _userRepository.GetUserById(dto.UserId);
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
    public ActionResult<bool> UpdateNoteDto(int id, UpdateNoteDto dto)
    {
        var user = _userRepository.GetUserById(dto.UserId);
        if (user is null)
        {
            throw new UserNotFoundExceptionId(dto.UserId);
        }
        var result = _noteRepository.ChangeNote(
            id, 
            dto.UserId, 
            dto.Title.Trim(), 
            dto.Priority, 
            dto.Description.Trim());
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