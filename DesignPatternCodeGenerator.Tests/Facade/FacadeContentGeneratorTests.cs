using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Facade;
using DesignPatternCodeGenerator.Tests.Facade.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Facade;

public class FacadeContentGeneratorTests
{
    [Theory]
    [MemberData(nameof(FacadeCompilationSources.GetSampleDataToContentTests), MemberType = typeof(FacadeCompilationSources))]
    internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
    {
        var methodGroups = GeneratorTestsHelper.GetMethodGroups(inputSource);

        var methodGroupsByAttributeText = GroupCollectionHelper.GroupCollectionByAttributeValueText(methodGroups);

        var result = FacadeContentGenerator.GenerateClass(methodGroupsByAttributeText.First());

        result.RemoveWhitespace().Should().Be(expectedSource.RemoveWhitespace());
    }
}
