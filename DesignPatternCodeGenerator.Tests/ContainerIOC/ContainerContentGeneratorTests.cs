using DesignPatternCodeGenerator.ContainerIOC;
using DesignPatternCodeGenerator.Tests.ContainerIOC.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;
namespace DesignPatternCodeGenerator.Tests.ContainerIOC;

public class ContainerContentGeneratorTests
{
    /*
     TODO Add ContainerContentGeneratorTests

    [Theory]
    [MemberData(nameof(ContainerCompilationSources.GetSampleDataToClassTests), MemberType = typeof(ContainerCompilationSources))]
    internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(inputSource);
        var compilation = GeneratorTestsHelper.CreateCompilation(inputSource);

        var result = ContainerContentGenerator.GenerateClass(classGroup, compilation);

        result.RemoveWhitespace().Should().Be(expectedSource.RemoveWhitespace());
    }

    */
}
