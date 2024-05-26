using Microsoft.EntityFrameworkCore;

namespace CartesianExplosion;

public static class InMemoryDatabase
{
    public static MyDbContext Create()
    {
        var context = new MyDbContext();

        context.Database.EnsureCreated();

        return context;
    }
}

public class MyDbContext : DbContext
{
    public DbSet<PrimaryTable> PrimaryTables { get; set; }
    public DbSet<JoinTable1> JoinTable1s { get; set; }
    public DbSet<JoinTable2> JoinTable2s { get; set; }
    public DbSet<JoinTable3> JoinTable3s { get; set; }
    public DbSet<JoinTable4> JoinTable4s { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var primaryTable = new PrimaryTable { Id = 1 };
        var joinTable1 = Enumerable.Range(0, 10).Select(i => new JoinTable1 { Id = i + 1, PrimaryTableId = 1 });
        var joinTable2 = Enumerable.Range(0, 100).Select(i => new JoinTable2 { Id = i + 1, PrimaryTableId = 1 });
        var joinTable3 = Enumerable.Range(0, 100).Select(i => new JoinTable3 { Id = i + 1, PrimaryTableId = 1 });
        var joinTable4 = Enumerable.Range(0, 100).Select(i => new JoinTable4 { Id = i + 1, PrimaryTableId = 1 });

        modelBuilder.Entity<PrimaryTable>().HasData(primaryTable);
        modelBuilder.Entity<JoinTable1>().HasData(joinTable1);
        modelBuilder.Entity<JoinTable2>().HasData(joinTable2);
        modelBuilder.Entity<JoinTable3>().HasData(joinTable3);
        modelBuilder.Entity<JoinTable4>().HasData(joinTable4);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = $"Data Source={Path.GetFullPath(Path.Combine("Database", "CartesianExplosion.db"))}";
        optionsBuilder.UseSqlite(dbPath);
    }
}

public class PrimaryTable
{
    public int Id { get; init; }
    public List<JoinTable1> JoinTable1s { get; init; } = [];
    public List<JoinTable2> JoinTable2s { get; init; } = [];
    public List<JoinTable3> JoinTable3s { get; init; } = [];
    public List<JoinTable4> JoinTable4s { get; init; } = [];
}

public class JoinTable1
{
    public int Id { get; init; }
    public int PrimaryTableId { get; init; }

}

public class JoinTable2
{
    public int Id { get; init; }
    public int PrimaryTableId { get; init; }
}

public class JoinTable3
{
    public int Id { get; init; }
    public int PrimaryTableId { get; init; }
}

public class JoinTable4
{
    public int Id { get; init; }
    public int PrimaryTableId { get; init; }
}
