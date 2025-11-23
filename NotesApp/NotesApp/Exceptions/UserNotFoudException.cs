namespace NotesApp.Exceptions;

public class UserNotFoundExceptionId(int id) : Exception($"User with Id: {id}  not found");