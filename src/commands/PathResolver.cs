using System.Runtime.InteropServices;

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

            if(File.Exists(filePath) && File.GetUnixFileMode(filePath).HasFlag(UnixFileMode.UserExecute))
            {
                return filePath;
            }
        }

        return null;
    }
}