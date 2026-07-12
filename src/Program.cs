class Program
{
    static void Main()
    {

        while(true)
        {
            Console.Write("$ ");

            var command = Console.ReadLine();

            switch(command!.Split(' ')[0])
            {
                case "exit":
                    return;
                default:
                    Console.WriteLine($"{command}: command not found");
                    continue;
            }
        }
    }
}
