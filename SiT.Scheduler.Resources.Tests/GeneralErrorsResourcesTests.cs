namespace SiT.Scheduler.Resources.Tests
{
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class GeneralErrorsResourcesTests
    {
        [Theory]
        [InlineData("en")]
        [InlineData("bg")]
        public void EntityDoesNotExistShouldReturnValue(string cultureIdentifier)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(cultureIdentifier);
            CultureInfo.CurrentUICulture = cultureInfo;
            
            var attributeType = typeof(LabelsProviderAttribute);
            var labelsProvidersAssembly = attributeType.Assembly;

            var labelsProviderTypes = labelsProvidersAssembly.GetTypes().Where(t => t.IsDefined(attributeType, inherit: false));
            foreach (var type in labelsProviderTypes)
            {
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(p => p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var label = (string)property.GetValue(null);
                    Assert.False(string.IsNullOrWhiteSpace(label));
                }
            }
        }
    }
}
