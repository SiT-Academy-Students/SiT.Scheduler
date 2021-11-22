namespace SiT.Scheduler.Tests.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using SiT.Scheduler.Utilities.Errors;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using Xunit;
    using Xunit.Abstractions;

    public class ErrorTests
    {
        private readonly ITestOutputHelper output;

        public ErrorTests(ITestOutputHelper output)
        {
            this.output = output ?? throw new ArgumentNullException(nameof(output));
        }

        [Theory]
        [MemberData(nameof(ConstructAllErrors))]
        public void GeneralErrors(Type errorType, object[] args)
        {
            this.output.WriteLine(CultureInfo.CurrentUICulture.Name);
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            var instantiatedError = Activator.CreateInstance(errorType, args);
            var error = Assert.IsAssignableFrom<IError>(instantiatedError);
            Assert.NotNull(error.ErrorMessage);
        }

        public static IEnumerable<object[]> ConstructAllErrors()
        {
            yield return new object[] { typeof(ArgumentNullError), Array.Empty<object>() };
            yield return new object[] { typeof(ErrorFromException), new object[] { new InvalidOperationException(RandomizationHelper.GetRandomString()) } };
            yield return new object[] { typeof(StandardError), new object[] { RandomizationHelper.GetRandomString() } };
        }
    }
}