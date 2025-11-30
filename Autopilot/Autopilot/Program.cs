using Autopilot.Model;
using System.Text;

namespace Autopilot;

public class Program
{
    private static readonly string _fileName = "autopilot.json";

    private static ExecutionMode _executionMode;

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Autopilot!");

        if (args.Length != 1)
        {
            Console.WriteLine(BuildErrorMessage());
            return;
        }

        if (!Enum.TryParse(args[0], true, out _executionMode))
        {
            Console.WriteLine(BuildErrorMessage());
            return;
        }

        try
        {
            var directiveHandler = new DirectiveHandler(_fileName);
            directiveHandler.ParseMagicVariables();

            var executor = new Executor(directiveHandler)
            {
                Mode = _executionMode
            };

            executor.Execute();
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid directive file given.");
            return;
        }
    }

    private static string BuildErrorMessage()
    {
        var errorMessage = new StringBuilder($"Usage: Autopilot <execution mode>. Valid execution modes: ");
        var modes = Enum.GetNames<ExecutionMode>();

        for (var i = 0; i < modes.Length; i++)
        {
            errorMessage.Append($"'{modes[i].ToLower()}'");
            if (i < modes.Length - 1)
            {
                errorMessage.Append(", ");
            }
        }

        return errorMessage.ToString();
    }
}
