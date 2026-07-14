using System.Runtime.InteropServices;
using static System.IO.File;

namespace CodeCrafters.Shell.Commands;

internal static class PathResolver
{
    public static string? Resolve(string command)
    {
        var pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (pathEnv == null)
        {
            return null;
        }

        var directories = pathEnv.Split(Path.PathSeparator);

        foreach(var directory in directories)
        {
            string filePath;

            try
            {
                filePath = Path.Combine(directory, command);
            }
            catch(ArgumentException)
            {
                continue;
            }

            if(Exists(filePath) && GetUnixFileMode(filePath).HasFlag(UnixFileMode.UserExecute))
            {
                return filePath;
            }
        }

        return null;
    }
}