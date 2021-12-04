namespace SiT.Scheduler.Repository.Tests.Integration;

using SiT.Scheduler.Repository.Tests.CollectionFixtures;
using SiT.Scheduler.Tests.Fixtures.Database;
using Xunit;
using Xunit.Abstractions;

[Collection(PostgreSqlCollectionFixture.CollectionIdentifier)]
public class PostgreSqlRepositoryTests : BaseRepositoryTest<PostgreSqlTestDatabaseProvider>
{
    public PostgreSqlRepositoryTests(PostgreSqlTestDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }
}
