namespace Autopilot;

public class Program
{
    private static DirectiveHandler _directiveHandler;

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Autopilot!");

        var directiveFile = args.Length > 0 ? args[0] : "autopilot.json";

        try
        {
            _directiveHandler = new DirectiveHandler(directiveFile);
            _directiveHandler.ParseMagicVariables();
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid directive file given.");
            return;
        }
    }
}
