namespace SiT.Scheduler.Tests.Extensions;

using SiT.Scheduler.Utilities.OperationResults;
using Xunit;

public static class AssertionsExtensions
{
    public static void AssertSuccess(this IOperationResult operationResult)
    {
        Assert.NotNull(operationResult);
        Assert.True(operationResult.IsSuccessful, operationResult.ToString());
    }
}
