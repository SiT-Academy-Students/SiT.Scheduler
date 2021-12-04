namespace SiT.Scheduler.Tests.OperationResult;

using SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Utilities.OperationResults;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public class OperationResultTests
{
    [Fact]
    public void OperationResultShouldBeInitiallySuccessful()
    {
        var operationResult = new OperationResult();

        Assert.True(operationResult.IsSuccessful);
        Assert.Equal(0, operationResult.Errors.Count);
    }

    [Fact]
    public void OperationResultShouldBeUnsuccessfulWhenErrorsAreAdded()
    {
        // Arrange
        var operationResult = new OperationResult();
        var standardError = new StandardError(RandomizationHelper.GetRandomString());

        // Act
        var addSuccess = operationResult.AddError(standardError);

        // Assert
        Assert.True(addSuccess);
        Assert.False(operationResult.IsSuccessful);
        Assert.Equal(1, operationResult.Errors.Count);
    }

    [Fact]
    public void AddErrorShouldReturnFalseIfErrorIsNull()
    {
        var operationResult = new OperationResult();

        var addSuccess = operationResult.AddError(null);
        Assert.False(addSuccess);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void OperationResultShouldStoreDataCorrectly(int data)
    {
        var operationResult = new OperationResult<int> { Data = data };
        Assert.Equal(operationResult.Data, data);
    }
}
