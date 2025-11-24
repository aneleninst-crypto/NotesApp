namespace NotesApp.Exceptions;

public class UserNotFoundByLoginException(string login) : Exception($"User with login {login} not found"); // лучше назвать UserNotFoundByLoginException. Само слово Exception всегда
                                                                                                         // должно быть в конце названия классов исключений 