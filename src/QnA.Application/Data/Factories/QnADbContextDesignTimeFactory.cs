namespace QnA.Application.Data.Factories;

public class QnADbContextDesignTimeFactory : IDesignTimeDbContextFactory<QnADbContext>
{
    public QnADbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QnADbContext>();
        //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=QnALocalDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        optionsBuilder.UseSqlServer("Server=db;Database=QnADb;User=sa;Password=QnADemoP@ssw0rd;");
        return new QnADbContext(optionsBuilder.Options);
    }
}
