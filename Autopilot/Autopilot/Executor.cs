using Autopilot.Model;

namespace Autopilot;

public class Executor(DirectiveHandler directiveHandler)
{
    private readonly DirectiveHandler _directiveHandler = directiveHandler;

    private readonly EnvironmentManager _environmentManager = directiveHandler.EnvironmentManager;

    public ExecutionMode Mode { get; set; }

    public void Execute()
    {
        if (Mode == ExecutionMode.Retrieve)
        {
            Retrieve(_directiveHandler.Directives);
        }
        else
        {
            Deploy(_directiveHandler.Directives);
        }
    }

    private void Retrieve(IList<Directive> directives)
    {
        var targetBaseDirectory = Path.Combine(
            Directory.GetCurrentDirectory(),
            _environmentManager.Environment
        );

        if (!Directory.Exists(targetBaseDirectory))
        {
            Directory.CreateDirectory(targetBaseDirectory);
        }

        foreach (var directive in directives)
        {
            if (directive.Environment != null && directive.Environment != _directiveHandler.EnvironmentManager.Environment)
            {
                continue;
            }

            var source = new FileInfo(Path.Combine(directive.Location, directive.File));
            var target = new FileInfo(Path.Combine(targetBaseDirectory, directive.Name, directive.File));

            if (!source.Exists)
            {
                Console.WriteLine($"Source file does not exist: {source.FullName}");
                continue;
            }

            if (target == source)
            {
                Console.WriteLine($"Source and target files are identical, skipping directive execution...");
                continue;
            }

            if (target.Exists)
            {
                target.Delete();
            }

            source.CopyTo(target.FullName);
        }
    }

    private void Deploy(IList<Directive> directives)
    {
        foreach (var directive in directives)
        {
            if (directive.Environment != null && directive.Environment != _directiveHandler.EnvironmentManager.Environment)
            {
                continue;
            }
        }
    }


}
