namespace SiT.Scheduler.Tests.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using SiT.Scheduler.Utilities.Errors;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using Xunit;

    public class ErrorTests
    {
        [Theory]
        [MemberData(nameof(ConstructAllErrors))]
        public void GeneralErrors(Type errorType, object[] args)
        {
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