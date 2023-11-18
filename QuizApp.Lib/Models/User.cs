using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Models;

public class User
{
    public string Username { get; set; } = null!;
    public UserRole UserRole { get; set; }

    public static implicit operator User(UserEntity user)
    {
        return new User()
        {
            Username = user.Username,
            UserRole = (UserRole)user.UserRoleID
        };
    }
}

public enum UserRole
{
    None,
    Registered,
    Guest
}
