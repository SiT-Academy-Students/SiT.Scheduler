namespace SiT.Scheduler.Tests.Helpers;

using System.Collections.Generic;
using System.Linq;

public static class TestsHelper
{
    public static IEnumerable<string> GetInvalidStringValues()
    {
        yield return null;
        yield return string.Empty;
        yield return "   ";
        yield return "\r";
        yield return "\n";
        yield return "\t";
        yield return " \r\n\t";
    }

    public static IEnumerable<object[]> GetInvalidStringData() => GetInvalidStringValues().Select(invalidStringValue => new object[] { invalidStringValue });
}
