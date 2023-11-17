namespace QuizApp;

internal class Application
{
    public Application()
    {
        
    }

    public async Task Run()
    {
        do
        {

            await Console.Out.WriteLineAsync("App running...");

            var b = Console.ReadKey().KeyChar;

            if (b == 'e') break;

        } while (true);
    }
}
