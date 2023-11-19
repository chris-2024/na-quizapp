using QuizApp.Lib.Enums;
using QuizApp.Lib.Services;

namespace QuizApp.Services;

internal class LoginService
{
    private readonly IUserService _userService;

    public LoginService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> LoginUser(string userName, string password)
    {
        return await _userService.GetUser(new() { Username = userName, Password = password });
    }

    public async Task<bool> RegisterUser(string userName, string password)
    {
        return await _userService.RegisterNewUser(new() { Username = userName, Password = password, UserRole = UserRole.Registered });
    }

    public async Task Guest()
    {
        await _userService.RegisterNewUser(new() { Username = "Guest_" + Guid.NewGuid().ToString(), UserRole = UserRole.Guest });
    }
}
