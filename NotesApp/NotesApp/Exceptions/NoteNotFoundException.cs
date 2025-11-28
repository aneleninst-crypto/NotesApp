namespace NotesApp.Exceptions;

public class NoteNotFoundException(int id) : Exception($"Note with id: {id} not found");