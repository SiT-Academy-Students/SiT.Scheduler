namespace SiT.Scheduler.Repository.Tests.Integration;

using SiT.Scheduler.Repository.Tests.CollectionFixtures;
using SiT.Scheduler.Repository.Tests.Data.DatabaseProviders;
using Xunit;
using Xunit.Abstractions;

[Collection(TestPostgreDatabaseFixture.CollectionIdentifier)]
public class GeneralRepositoryPostgreSqlTest : GeneralRepositoryTest<TestPostgreDatabaseProvider>
{
    public GeneralRepositoryPostgreSqlTest(TestPostgreDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }
}
