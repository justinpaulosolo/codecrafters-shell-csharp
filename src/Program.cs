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

                    string? path = Environment.GetEnvironmentVariable("PATH");
                    char pathSeparator = Path.PathSeparator; 
                    
                    string[] paths = path.Split(pathSeparator);

                    foreach(string p in paths)
                    {
                        string fullPath = Path.Combine(p, type);
                        if (File.Exists(fullPath))
                        {
                            Console.WriteLine($"type is {path}", p);
                            break;
                        }
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
