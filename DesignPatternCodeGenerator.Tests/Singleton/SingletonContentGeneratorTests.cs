using DesignPatternCodeGenerator.Singleton;
using DesignPatternCodeGenerator.Tests.Helpers;
using DesignPatternCodeGenerator.Tests.Singleton.Data;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Singleton;

public class SingletonContentGeneratorTests
{
    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToClassTests), MemberType = typeof(SingletonCompilationSources))]
    internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(inputSource);

        var result = SingletonContentGenerator.GenerateClass(classGroup);

        result.RemoveWhitespace().Should().Be(expectedSource.RemoveWhitespace());
    }
}
