using System.Diagnostics;

class Program
{
    static void Main()
    {

        while(true)
        {
            Console.Write("$ ");

            var command = Console.ReadLine();
            var splitResult = command!.Split(" ");

            switch(splitResult[0])
            {
                case "type":
                    var type = string.Join(" ", splitResult[1..]);

                    if (type == "echo" || type =="exit" || type == "type" )
                    {
                        Console.WriteLine($"{type} is a shell builtin", type);
                        break;
                    }

                    string? pathEnv = Environment.GetEnvironmentVariable("PATH");

                    string[] directories = pathEnv!.Split(Path.PathSeparator);

                    bool found = false;

                    foreach(string directory in directories)
                    {
                        string filePath = Path.Combine(directory, type);

                        if (File.Exists(filePath) && File.GetUnixFileMode(filePath).HasFlag(UnixFileMode.UserExecute))
                        {
                            Console.WriteLine($"{type} is {filePath}");
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"{type} not found", type);
                    }

                    break;
                case "echo":
                    var msg = string.Join(" ", splitResult[1..]);
                    Console.WriteLine(msg);
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine($"{command}: command not found");
                    continue;
            }
        }
    }
}
