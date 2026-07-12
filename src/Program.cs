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
