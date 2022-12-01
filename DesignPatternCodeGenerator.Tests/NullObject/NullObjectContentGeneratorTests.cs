using DesignPatternCodeGenerator.NullObject;
using DesignPatternCodeGenerator.Tests.Helpers;
using DesignPatternCodeGenerator.Tests.NullObject.Data;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.NullObject;

public class NullObjectContentGeneratorTests
{
    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToClassTests), MemberType = typeof(NullObjectCompilationSources))]
    internal void GenerateMainInterface_ForValidInputs_ReturnsCorrectMainInterface(string inputSource, string expectedInterface)
    {
        var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroup(inputSource);

        var result = NullObjectContentGenerator.GenerateClass(interfaceGroup);

        result.RemoveWhitespace().Should().Be(expectedInterface.RemoveWhitespace());
    }
}
