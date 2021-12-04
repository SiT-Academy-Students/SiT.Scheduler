namespace SiT.Scheduler.Repository.Tests.CollectionFixtures;

using SiT.Scheduler.Tests.Fixtures.Database;
using Xunit;

[CollectionDefinition(CollectionIdentifier)]
public class PostgreSqlCollectionFixture : ICollectionFixture<PostgreSqlTestDatabaseProvider>
{
    public const string CollectionIdentifier = "postgre_sql";
}
