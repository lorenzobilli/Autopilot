using Autopilot.Model;
using System.Text;

namespace Autopilot;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Autopilot!");

        if (args.Length != 2)
        {
            Console.WriteLine(BuildErrorMessage());
            return;
        }

        if (!Enum.TryParse(args[1], true, out ExecutionMode executionMode))
        {
            Console.WriteLine(BuildErrorMessage());
            return;
        }

        try
        {
            var directiveHandler = new DirectiveHandler(args[0]);

            directiveHandler.ParseMagicVariables();

            var executor = new Executor(directiveHandler)
            {
                Mode = executionMode
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
        var errorMessage = new StringBuilder($"Usage: Autopilot <directives .json file> <execution mode>. Valid execution modes: ");
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
