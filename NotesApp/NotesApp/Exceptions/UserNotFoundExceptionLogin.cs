namespace NotesApp.Exceptions;

public class UserNotFoundExceptionLogin(string login) : Exception($"User with login {login} not found");