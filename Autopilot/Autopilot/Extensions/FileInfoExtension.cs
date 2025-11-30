using System.Security.Cryptography;

namespace Autopilot.Extensions;

public static class FileInfoExtension
{
    public static bool Identical(this FileInfo source, FileInfo other)
    {
        if (source is null || other is null)
        {
            return false;
        }

        if (!source.Exists || !other.Exists)
        {
            return false;
        }

        var leftHash = MD5.Create().ComputeHash(source.OpenRead());
        var rightHash = MD5.Create().ComputeHash(other.OpenRead());

        return leftHash.SequenceEqual(rightHash);
    }
}
