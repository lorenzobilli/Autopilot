using System.Security.Cryptography;

namespace Autopilot.Extensions;

public static class FileInfoExtension
{
    extension(FileInfo? source)
    {
        public static bool operator ==(FileInfo? left, FileInfo? right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            if (!left.Exists || !right.Exists)
            {
                return false;
            }

            var leftHash = MD5.Create().ComputeHash(left.OpenRead());
            var rightHash = MD5.Create().ComputeHash(right.OpenRead());

            return leftHash.SequenceEqual(rightHash);
        }

        public static bool operator !=(FileInfo? left, FileInfo? right)
        {
            return !(left == right);
        }
    }
}
