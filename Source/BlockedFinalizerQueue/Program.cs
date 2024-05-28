// Example program that demonstrates how the finalizer queue can be blocked by a long running finalizer causing a memory leak
// Try to run the program with the finalizer commented out and see how the memory usage doesn't grow

var exit = false;

while (!exit)
{
    GeneratePeople();

    Console.WriteLine($"Memory used: {GC.GetTotalMemory(false):N0} bytes");
    Console.WriteLine("Leak more memory (y/n)");

    var answer = Console.ReadKey();
    exit = answer.Key != ConsoleKey.Y;
    Console.WriteLine();
}

Console.WriteLine("Bye!");

return;

void GeneratePeople()
{
    var people = new List<Person>();

    for (var i = 0; i < 10_000; i++)
    {
        people.Add(new Person(
            id: i,
            firstName: $"John #{i}",
            lastName: "Doe"));
    }
}

internal sealed class Person
{
    internal Person(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    private char[] Details { get; } = new char[10_000];

    ~Person()
    {
        Console.WriteLine($"Finalizing person {Id}");
        Thread.Sleep(5000); // Simulate a long operation
    }
}