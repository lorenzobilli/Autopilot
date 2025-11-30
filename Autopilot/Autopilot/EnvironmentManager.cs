using System.Runtime.InteropServices;

namespace Autopilot;

public class EnvironmentManager
{
    public string Environment { get; private set; }

    public EnvironmentManager(string? executionEnvironment)
    {
        if (executionEnvironment != null)
        {
            Environment = executionEnvironment;
        }
        else
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Environment = "Windows";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Environment = "Linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Environment = "macOS";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                Environment = "FreeBSD";
            }
            else
            {
                throw new ApplicationException("Unsupported operating system platform.");
            }
        }
    }
}
