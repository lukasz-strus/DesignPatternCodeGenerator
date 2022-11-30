using DesignPatternCodeGenerator.Prototype;
using DesignPatternCodeGenerator.Tests.Factory.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using DesignPatternCodeGenerator.Tests.Prototype.Data;
using FluentAssertions;
using Xunit;
namespace DesignPatternCodeGenerator.Tests.Prototype;

public class PrototypeContentComponentsGeneratorTests
{
    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToDeepMethodTests), MemberType = typeof(PrototypeCompilationSources))]
    internal void GenerateDeepClone_ForValidInputs_ReturnDeepCloneMethod(string inputSource, string methodSource)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(inputSource);
        var allClassGroup = GeneratorTestsHelper.GetClassGroups(inputSource);

        var result = PrototypeContentComponentsGenerator.GenerateDeepClone(classGroup, allClassGroup);
        
        result.RemoveWhitespace().Should().Be(methodSource.RemoveWhitespace());
    }

    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToShallowMethodTests), MemberType = typeof(PrototypeCompilationSources))]
    internal void GenerateShallowClone_ForValidInputs_ReturnShallowCloneMethod(string inputSource, string methodSource)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(inputSource);

        var result = PrototypeContentComponentsGenerator.GenerateShallowClone(classGroup);

        result.RemoveWhitespace().Should().Be(methodSource.RemoveWhitespace());
    }
}
