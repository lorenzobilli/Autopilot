using Autopilot.Extensions;
using Autopilot.Model;

namespace Autopilot;

public class Executor
{
    private readonly string WorkingDirectory;

    private readonly DirectiveHandler _directiveHandler;

    private readonly EnvironmentManager _environmentManager;

    public Executor(DirectiveHandler directiveHandler, EnvironmentManager environmentManager)
    {
        _directiveHandler = directiveHandler;
        _environmentManager = environmentManager;
        WorkingDirectory = new FileInfo(_directiveHandler.DirectivesFile).DirectoryName ??
            throw new ApplicationException("Unable to retrieve working directory from given directives file");
    }

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
            WorkingDirectory,
            _environmentManager.Environment
        );

        if (!Directory.Exists(targetBaseDirectory))
        {
            Directory.CreateDirectory(targetBaseDirectory);
        }

        foreach (var directive in directives)
        {
            if (directive.Environment != null && directive.Environment != _environmentManager.Environment)
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

            if (source.Identical(target))
            {
                Console.WriteLine($"Source and target files are identical, skipping directive execution...");
                continue;
            }

            if (!Directory.Exists(target.DirectoryName))
            {
                Directory.CreateDirectory(target.DirectoryName!);
            }

            var stream = target.OpenRead();
            stream.Close();

            File.Copy(source.FullName, target.FullName, overwrite: true);
        }
    }

    private void Deploy(IList<Directive> directives)
    {
        var sourceBaseDirectory = Path.Combine(
            WorkingDirectory,
            _environmentManager.Environment
        );

        foreach (var directive in directives)
        {
            if (directive.Environment != null && directive.Environment != _environmentManager.Environment)
            {
                continue;
            }

            var source = new FileInfo(Path.Combine(sourceBaseDirectory, directive.Name, directive.File));
            var target = new FileInfo(Path.Combine(directive.Location, directive.File));

            if (!source.Exists)
            {
                Console.WriteLine($"Source file does not exist: {source.FullName}");
                continue;
            }

            if (source.Identical(target))
            {
                Console.WriteLine($"Source and target files are identical, skipping directive execution...");
                continue;
            }

            if (!Directory.Exists(target.DirectoryName))
            {
                Directory.CreateDirectory(target.DirectoryName!);
            }

            var stream = target.OpenRead();
            stream.Close();

            File.Copy(source.FullName, target.FullName, overwrite: true);
        }
    }
}
