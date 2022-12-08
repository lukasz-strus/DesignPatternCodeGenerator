using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Facade.Compontents;
using DesignPatternCodeGenerator.Tests.Facade.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Facade
{
    public class FacadeContentFieldsGeneratorTests
    {
        [Theory]
        [MemberData(nameof(FacadeCompilationSources.GetSampleDataToFieldsTests), MemberType = typeof(FacadeCompilationSources))]
        internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
        {
            var methodGroups = GeneratorTestsHelper.GetMethodGroups(inputSource);

            var result = FacadeFieldsComponentsGenerator.GenerateFileds(methodGroups.GroupByAttribute().First());

            result.RemoveWhitespace()
                  .Should()
                  .Be(expectedSource.RemoveWhitespace());
        }
    }
}
