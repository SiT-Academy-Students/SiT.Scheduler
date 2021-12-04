namespace SiT.Scheduler.Tests.Errors;

using System;
using SiT.Scheduler.Utilities.Errors;
using Xunit;

public class ErrorFromExceptionTests
{
    [Fact]
    public void InstantiatingErrorFromExceptionWithInvalidArgumentsShouldFail() => Assert.Throws<ArgumentNullException>(() => new ErrorFromException(null));
}
