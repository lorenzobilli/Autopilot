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
        Console.WriteLine($"[*] Discovered N° {_directiveHandler.Directives.Count} directives to process.");

        if (Mode == ExecutionMode.Retrieve)
        {
            Console.WriteLine("[*] Executing directives in retrieve mode...");
            Retrieve(_directiveHandler.Directives);
        }
        else
        {
            Console.WriteLine("[*] Executing directories in deploy mode...");
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
            Console.WriteLine($"[*] Executing directive {directive.Name ?? string.Empty}...");

            if (directive.Environment != null && directive.Environment != _environmentManager.Environment)
            {
                Console.WriteLine("-> [!] Skipping directive due to incompatible environment");
                continue;
            }

            var source = new FileInfo(Path.Combine(directive.Location, directive.File));
            var target = new FileInfo(Path.Combine(targetBaseDirectory, directive.Group, directive.File));

            if (!source.Exists)
            {
                Console.WriteLine($"-> [!] Source file does not exist: {source.FullName}");
                continue;
            }

            if (source.Identical(target))
            {
                Console.WriteLine($"-> [!] Source and target files are identical, skipping directive execution...");
                continue;
            }

            if (!Directory.Exists(target.DirectoryName))
            {
                Directory.CreateDirectory(target.DirectoryName!);
            }

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
            Console.WriteLine($"[*] Executing directive {directive.Name ?? string.Empty}...");

            if (directive.Environment != null && directive.Environment != _environmentManager.Environment)
            {
                Console.WriteLine("-> [!] Skipping directive due to incompatible environment");
                continue;
            }

            var source = new FileInfo(Path.Combine(sourceBaseDirectory, directive.Group, directive.File));
            var target = new FileInfo(Path.Combine(directive.Location, directive.File));

            if (!source.Exists)
            {
                Console.WriteLine($"-> [!] Source file does not exist: {source.FullName}");
                continue;
            }

            if (source.Identical(target))
            {
                Console.WriteLine($"-> [!] Source and target files are identical, skipping directive execution...");
                continue;
            }

            if (!Directory.Exists(target.DirectoryName))
            {
                Directory.CreateDirectory(target.DirectoryName!);
            }

            File.Copy(source.FullName, target.FullName, overwrite: true);
        }
    }
}
