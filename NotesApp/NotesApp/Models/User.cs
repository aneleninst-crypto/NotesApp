namespace NotesApp.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public User (int id, string login, string password) // убери конструкторы, они тебе пока ни к чему.
        // Ты создаешь сущности только в 1 месте своего приложения - репозитории
        // при маппинге. А значит в теории нам конструктор (инструкция по созданию)
        // не нужна. Как только понадобятся, ты поймешь и используешь.
    {
        Id = id;
        Login = login;
        Password = password;
    }
}