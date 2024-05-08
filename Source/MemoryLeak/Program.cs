// Simple program that leaks memory by creating a list of objects that are never released.

var persons = new List<Person>();
var exit = false;

while (!exit)
{
    foreach (var i in Enumerable.Range(0, 10_000))
    {
        persons.Add(new Person(
            id: persons.Count,
            firstName: $"John #{persons.Count}",
            lastName: "Doe"));
    }

    Console.WriteLine("Leak more memory (y/n)");
    var answer = Console.ReadLine();
    exit = answer != "y";
}

Console.WriteLine("Bye!");

internal class Person
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

