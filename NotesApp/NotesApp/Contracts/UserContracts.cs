namespace NotesApp.Contracts;

public record UserVm(Guid Id, string Login);
public record UserListVm(string Login);
public record ListOfUsers(List<UserListVm> Users);
public record CreateUserDto(string Login, string Password);
public record UpdateUserDto(string Login);
public record LogInDto (string Login, string Password);