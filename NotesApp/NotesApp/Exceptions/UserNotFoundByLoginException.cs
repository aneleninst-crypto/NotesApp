namespace NotesApp.Exceptions;

public class UserNotFoundByLoginException(string login) : Exception($"User with login {login} not found");