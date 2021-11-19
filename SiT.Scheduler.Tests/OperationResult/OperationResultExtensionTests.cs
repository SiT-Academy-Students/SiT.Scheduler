namespace SiT.Scheduler.Tests.OperationResult
{
    using SiT.Scheduler.Utilities.Errors;
    using SiT.Scheduler.Utilities.OperationResults;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using Xunit;

    public class OperationResultExtensionTests
    {
        [Fact]
        public void ValidatingNullabilityShouldWorkCorrectlyWhenValueIsNull()
        {
            var operationResult = new OperationResult();

            operationResult.ValidateNotNull<object>(null);
            Assert.False(operationResult.IsSuccessful);
            var error = Assert.Single(operationResult.Errors);
            Assert.IsType<ArgumentNullError>(error);
        }

        [Fact]
        public void ValidatingNullabilityShouldWorkCorrectlyWithCustomErrorWhenValueIsNull()
        {
            var operationResult = new OperationResult();

            var customError = new StandardError(RandomizationHelper.GetRandomString());
            operationResult.ValidateNotNull<object>(null, customError);
            Assert.False(operationResult.IsSuccessful);
            var storedError = Assert.Single(operationResult.Errors);
            Assert.Same(customError, storedError);
        }

        [Fact]
        public void ValidatingNullabilityShouldWorkCorrectlyWhenValueIsNotNull()
        {
            var operationResult = new OperationResult();

            operationResult.ValidateNotNull(new object());
            Assert.True(operationResult.IsSuccessful);
            Assert.Empty(operationResult.Errors);
        }
    }
}