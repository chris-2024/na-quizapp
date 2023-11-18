namespace QuizApp.Services;

internal class LoginService
{
    public LoginService()
    {
        
    }

    public async Task LoginUser(string userName, string password)
    {
        await Console.Out.WriteLineAsync(userName + " logged in");
    }

    public async Task RegisterUser(string userName, string password)
    {
        await Console.Out.WriteLineAsync(userName + " registered");
    }

    public async Task Guest()
    {
        await Console.Out.WriteLineAsync("Guest");
    }
}
