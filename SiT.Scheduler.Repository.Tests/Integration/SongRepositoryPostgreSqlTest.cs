namespace SiT.Scheduler.Repository.Tests.Integration;

using SiT.Scheduler.Repository.Tests.CollectionFixtures;
using SiT.Scheduler.Tests.Fixtures.Database;
using Xunit;
using Xunit.Abstractions;

[Collection(PostgreSqlCollectionFixture.CollectionIdentifier)]
public class SongRepositoryPostgreSqlTest : SongRepositoryTest<PostgreSqlTestDatabaseProvider>
{
    public SongRepositoryPostgreSqlTest(PostgreSqlTestDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }
}
