using System.Diagnostics;

class Program
{
    static void Main()
    {

        while(true)
        {
            Console.Write("$ ");

            var userInput = Console.ReadLine();
            var splitResult = userInput!.Split(" ");


            string? pathEnv = Environment.GetEnvironmentVariable("PATH");

            string[] directories = pathEnv!.Split(Path.PathSeparator);


            switch(splitResult[0])
            {
                case "type":
                    var command = string.Join(" ", splitResult[1..]);

                    if (command == "echo" || command =="exit" || command == "type" )
                    {
                        Console.WriteLine($"{command} is a shell builtin", command);
                        break;
                    }

                    bool found = false;

                    foreach(string directory in directories)
                    {
                        string filePath = Path.Combine(directory, command);

                        if (File.Exists(filePath) && File.GetUnixFileMode(filePath).HasFlag(UnixFileMode.UserExecute))
                        {
                            Console.WriteLine($"{command} is {filePath}");
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"{command} not found", command);
                    }

                    break;
                case "echo":
                    var msg = string.Join(" ", splitResult[1..]);
                    Console.WriteLine(msg);
                    break;
                case "exit":
                    return;
                default:
                    string? resolvedPath = null;

                    foreach(string directory in directories)
                    {
                        string filePath = Path.Combine(directory, splitResult[0]);

                        if (File.Exists(filePath) && File.GetUnixFileMode(filePath).HasFlag(UnixFileMode.UserExecute))
                        {
                            resolvedPath = filePath;
                            break;
                        }
                    }
                    if (resolvedPath == null)
                    {
                        Console.WriteLine($"{userInput}: command not found");
                        break;
                    }


                    string[]? args = splitResult[1..];

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = splitResult[0],
                        Arguments = args.ToString(),
                        UseShellExecute = false,
                    };

                    foreach(string a in args)
                    {
                        startInfo.ArgumentList.Add(a);
                    }

                    using (Process process = Process.Start(startInfo))
                    {
                        Console.WriteLine($"Program was passed {args.Length + 1} args (including program name).");
                        Console.WriteLine($"Arg #0 (program name): {splitResult[0]}");
                        for(int i = 0; i < args.Length; i++)
                        {
                            Console.WriteLine($"Arg #{i+1}: {args[i]}");
                        }
                        process.WaitForExit();
                    }

                    break;
            }
        }
    }
}