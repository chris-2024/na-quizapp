using QuizApp.Lib.Enums;
using QuizApp.Lib.Models.Registrations;
using QuizApp.Lib.Services;

namespace QuizApp.Services;

internal class LoginService
{
    private readonly IUserService _userService;

    public LoginService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> LoginUser()
    {
        var user = UserFieldsInput();

        if (user == null) return false;

        return await _userService.GetUserAsync(user);
    }

    public async Task<bool> RegisterUser()
    {
        var user = UserFieldsInput();

        if (user == null) return false;

        user.UserRole = UserRole.Registered;

        return await _userService.RegisterNewUserAsync(user);
    }

    public async Task Guest()
    {
        await _userService.RegisterNewUserAsync(new() { Username = "Guest_" + Guid.NewGuid().ToString(), UserRole = UserRole.Guest });
    }

    private UserRegistration UserFieldsInput()
    {
        var user = new UserRegistration();

        Console.Clear();

        Console.Write("Username: ");
        user.Username = Console.ReadLine() ?? string.Empty;
        Console.Write("Password: ");
        user.Password = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
        {
            Console.Write("\nInvalid input");
            return null!;
        }

        return user;
    }
}
