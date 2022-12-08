using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Tests.Base.Generators.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.Generators;

public class BaseCodeGeneratorTests
{
    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataUsingsAndNamespace), MemberType = typeof(BaseCompilationSources))]
    public void GenerateUsingsAndNamespace_ForValidInputs_ReturnsCorrectString(string source, string expected, string additionalSource)
    {
        var interfaceGroup = GeneratorTestsHelper.GetClassGroup(source);

        string result;

        if (additionalSource != null)
        {
            var additionalInterfaceGroup = GeneratorTestsHelper.GetInterfaceGroup(additionalSource);
            result = BaseCodeGenerator.GenerateUsingsAndNamespace(interfaceGroup, additionalInterfaceGroup);
        }
        else
        {
            result = BaseCodeGenerator.GenerateUsingsAndNamespace(interfaceGroup);
        }

        result.RemoveWhitespace()
              .Should()
              .Be(expected.RemoveWhitespace());
    }

}
