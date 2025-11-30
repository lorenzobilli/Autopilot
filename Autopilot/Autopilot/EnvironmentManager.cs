using System.Runtime.InteropServices;

namespace Autopilot;

public class EnvironmentManager
{
    public string ExecutionEnvironment { get; private set; }

    public EnvironmentManager(string? executionEnvironment)
    {
        if (executionEnvironment != null)
        {
            ExecutionEnvironment = executionEnvironment;
        }
        else
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ExecutionEnvironment = "Windows";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ExecutionEnvironment = "Linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                ExecutionEnvironment = "macOS";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                ExecutionEnvironment = "FreeBSD";
            }
            else
            {
                throw new ApplicationException("Unsupported operating system platform.");
            }
        }
    }
}
