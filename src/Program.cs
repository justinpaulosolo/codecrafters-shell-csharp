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

                    foreach(string directory in directories)
                    {
                        if (string.IsNullOrWhiteSpace(directory)) continue;;

                        try
                        {
                            string fullPath = Path.Combine(directory, type);

                            if (File.Exists(fullPath))
                            {
                                Console.WriteLine($"{type} is {fullPath}");
                                break;
                            }
                        }
                        catch(ArgumentException){};

                    }

                    Console.WriteLine($"{type} not found", type);
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
