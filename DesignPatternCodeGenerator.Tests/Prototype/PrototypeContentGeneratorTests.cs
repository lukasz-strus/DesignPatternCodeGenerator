using DesignPatternCodeGenerator.Prototype;
using DesignPatternCodeGenerator.Tests.Helpers;
using DesignPatternCodeGenerator.Tests.Prototype.Data;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Prototype;

public class PrototypeContentGeneratorTests
{
    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToClassTests), MemberType = typeof(PrototypeCompilationSources))]
    internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(inputSource);
        var allClassGroup = GeneratorTestsHelper.GetClassGroups(inputSource);

        var result = PrototypeContentGenerator.GenerateClass(classGroup, allClassGroup);

        result.RemoveWhitespace().Should().Be(expectedSource.RemoveWhitespace());
    }
}
