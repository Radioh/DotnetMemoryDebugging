// Example program that demonstrates a Cartesian Product Explosion in EF Core when using Include on a very small amount of data
// 1 PrimaryTable -> 10 JoinTable1 -> 100 JoinTable2 -> 100 JoinTable3 -> 100 JoinTable4 = 10.000.000 rows
// Even removing the JoinTable4 from the query, the query will still produce 100.000 rows
// LINQ Method Syntax will only generate 1 result object, but the underlying SQL query will return 10.000.000 rows
// using LINQ Query Syntax will generate the 10.000.000 objects using the same underlying SQL query
// Try to add "AsSplitQuery()" to the Include method to avoid the Cartesian Explosion and notice the speed difference

using CartesianExplosion;
using Microsoft.EntityFrameworkCore;

var dbContext = InMemoryDatabase.Create();

Console.WriteLine("Cartesian Explosion using Entity Framework example");

var lingMethodSyntaxQuery = dbContext.PrimaryTables
    .Where(x => x.Id == 1)
    .Include(x => x.JoinTable1s)
    .Include(x => x.JoinTable2s)
    .Include(x => x.JoinTable3s)
    .Include(x => x.JoinTable4s) // Remove this line to adjust "explosion" from 10.000.000 to 100.000 rows
    //.AsSplitQuery() // Uncomment this line to avoid Cartesian Explosion - Notice the speed difference.
    ;

var lingQuerySyntaxQuery =
    from primaryTable in dbContext.PrimaryTables where primaryTable.Id == 1
    from joinTable1 in dbContext.JoinTable1s.Where(p => p.PrimaryTableId == primaryTable.Id).DefaultIfEmpty()
    from joinTable2 in dbContext.JoinTable2s.Where(c => c.PrimaryTableId == primaryTable.Id).DefaultIfEmpty()
    from joinTable3 in dbContext.JoinTable3s.Where(u => u.PrimaryTableId == primaryTable.Id).DefaultIfEmpty()
    from joinTable4 in dbContext.JoinTable4s.Where(d => d.PrimaryTableId == primaryTable.Id).DefaultIfEmpty() // Remove this line to adjust "explosion" from 10.000.000 to 100.000 rows
    orderby primaryTable.Id, joinTable1.Id, joinTable2.Id, joinTable3.Id, joinTable4.Id
    select new { primaryTable, joinTable1, joinTable2, joinTable3, joinTable4 };

var lingMethodSyntaxQuerySqlString = lingMethodSyntaxQuery.ToQueryString();
var lingQuerySyntaxQuerySqlString = lingQuerySyntaxQuery.ToQueryString();

Console.WriteLine("Generated SQL from query (Method Syntax):");
Console.WriteLine("=====================================");
Console.WriteLine(lingMethodSyntaxQuerySqlString);
Console.WriteLine("=====================================");

Console.WriteLine("Generated SQL from query (Query Syntax):");
Console.WriteLine("=====================================");
Console.WriteLine(lingQuerySyntaxQuerySqlString);
Console.WriteLine("=====================================");

Console.WriteLine("Execute Query syntax query or Method syntax query? (q = query/m = Method)");
var answer = Console.ReadKey();

Console.WriteLine("\n\nIf queries are taking too long, but you still want to debug explosion example, then remove .Include on JoinTable4 from the query.");

if (answer.Key == ConsoleKey.M)
{
    Console.WriteLine("Executing Method Syntax query... This might take a while...");
    var methodResult = await lingMethodSyntaxQuery.ToListAsync(); // Result count: 1
    Print(() => methodResult.Count);
}
else
{
    Console.WriteLine("Executing Query Syntax query... This might take a while...");
    var queryResult = await lingQuerySyntaxQuery.ToListAsync(); // Result count: 10.000.000
    Print(() => queryResult.Count);
}

Console.WriteLine("Bye");
return;

void Print(Func<int> countFunction) // countFunction is a delegate to make sure the result is not garbage collected before a memory dump is taken
{
    Console.WriteLine("Successfully executed query");
    Console.WriteLine("Take a memory dump now and/or use the SQL profiler in your IDE.");
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
    Console.WriteLine($"Result count: {countFunction():N0}");
}


