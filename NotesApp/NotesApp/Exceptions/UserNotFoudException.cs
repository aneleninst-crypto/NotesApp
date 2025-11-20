namespace NotesApp.Exceptions;

public class UserNotFoundException(int id) : Exception($"Note with Id: {id}  not found");