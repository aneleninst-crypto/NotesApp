namespace NotesApp.Contracts.AuthContracts;

public record LogInResponse (Guid UserId, string Token, string RefreshToken);