// Keep adding more and more objects to the pinned list and see how the memory usage grows.

var people = new List<Person>();
var exit = false;

while (!exit)
{
    for (var i = 0; i < 10_000; i++)
    {
        people.Add(new Person(
            id: i,
            firstName: $"John #{i}",
            lastName: "Doe"));
    }

    Console.WriteLine($"Person count: {people.Count:N0}");
    Console.WriteLine($"Memory used: {GC.GetTotalMemory(false):N0} bytes");
    Console.WriteLine("Leak more memory (y/n)");

    var answer = Console.ReadLine();
    exit = answer != "y";
}

Console.WriteLine("Bye!");

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
}

