namespace SiT.Scheduler.Tests.Errors
{
    using System;
    using SiT.Scheduler.Tests.Helpers;
    using SiT.Scheduler.Utilities.Errors;
    using Xunit;

    public class StandardErrorTests
    {
        [Theory]
        [MemberData(nameof(TestsHelper.GetInvalidStringData), MemberType = typeof(TestsHelper))]
        public void InstantiatingStandardErrorWithInvalidArgumentsShouldFail(string invalidString) => Assert.Throws<ArgumentNullException>(() => new StandardError(invalidString));
    }
}