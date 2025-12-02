namespace NotesApp.Contracts;

public record LogInResponse (Guid UserId, string Token, string RefreshToken);