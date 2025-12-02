namespace NotesApp.Exceptions;

public class UserNotFoundExceptionId(Guid id) : Exception($"User with Id: {id}  not found");