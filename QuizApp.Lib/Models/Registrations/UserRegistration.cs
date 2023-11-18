using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Models.Registrations;

public class UserRegistration
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole UserRole { get; set; }
}
