namespace SiT.Scheduler.Tests.Interface;

using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

public interface ITestDatabaseProvider
{
    void SetupDbContext(IServiceCollection serviceCollection, ITestOutputHelper testOutputHelper);
}
