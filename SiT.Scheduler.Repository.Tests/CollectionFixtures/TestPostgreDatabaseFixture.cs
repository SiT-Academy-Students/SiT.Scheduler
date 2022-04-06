namespace SiT.Scheduler.Repository.Tests.CollectionFixtures;

using SiT.Scheduler.Repository.Tests.Data.DatabaseProviders;
using Xunit;

[CollectionDefinition(CollectionIdentifier)]
public class TestPostgreDatabaseFixture : ICollectionFixture<TestPostgreDatabaseProvider>
{
    public const string CollectionIdentifier = "postgre_sql_fake_db";
}

