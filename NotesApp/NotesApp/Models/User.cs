namespace NotesApp.Models;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Note> Notes { get; set; } = [];
}