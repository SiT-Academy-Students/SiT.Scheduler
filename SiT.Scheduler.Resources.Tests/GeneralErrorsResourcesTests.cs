namespace SiT.Scheduler.Resources.Tests;

using System.Globalization;
using System.Linq;
using System.Reflection;
using Xunit;

public class GeneralErrorsResourcesTests
{
    [Theory]
    [InlineData("en")]
    [InlineData("bg")]
    public void AllLabelsShouldReturnValueWithSpecificCulture(string cultureIdentifier)
    {
        var cultureInfo = CultureInfo.GetCultureInfo(cultureIdentifier);
        CultureInfo.CurrentUICulture = cultureInfo;

        ValidateLabelsInternally();
    }

    [Fact]
    public void AllLabelsShouldReturnValueWithInvariantCulture()
    {
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        ValidateLabelsInternally();
    }

    private static void ValidateLabelsInternally()
    {
        var attributeType = typeof(LabelsProviderAttribute);
        var providersAssembly = attributeType.Assembly;

        var providerTypes = providersAssembly.GetTypes().Where(t => t.IsDefined(attributeType, inherit: false));
        foreach (var type in providerTypes)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(p => p.PropertyType == typeof(string));
            foreach (var property in properties)
            {
                var label = (string)property.GetValue(null);
                Assert.False(string.IsNullOrWhiteSpace(label));
            }
        }
    }
}
