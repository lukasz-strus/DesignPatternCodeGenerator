using DesignPatternCodeGenerator.Attributes.Factory;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.Generators;

public class DeclarationsSyntaxGeneratorTests
{
    [Fact]
    internal void GetInterfaceGroups_ForValidInputs_ReturnsCorrectKeyValue()
    {
        var compilation = GeneratorTestsHelper.CreateCompilation(FACTORY_COMPILATION_SOURCE);
        var source = new CancellationTokenSource();
        var token = source.Token;

        var result = DeclarationsSyntaxGenerator.GetInterfaceGroups(compilation, token, typeof(FactoryAttribute));

        result.Select(x => x.Key)
            .First()
            .Should()
            .Be("ITest");
    }

    [Fact]
    internal void GetClassGroups_ForValidInputs_ReturnsCorrectKeyValue()
    {
        var compilation = GeneratorTestsHelper.CreateCompilation(FACTORY_COMPILATION_SOURCE);
        var source = new CancellationTokenSource();
        var token = source.Token;

        var result = DeclarationsSyntaxGenerator.GetClassGroups(compilation, token, typeof(FactoryChildAttribute));

        result.Select(x => x.Key)
            .First()
            .Should()
            .Be("Test");
    }

    private const string FACTORY_COMPILATION_SOURCE =
    @"using DesignPatternCodeGenerator.Attributes.Factory;
using System;

namespace DesignPatternCodeGenerator.Tests.Data
{

    [Factory]
    public interface ITest
    {

    }

    [FactoryChild]
    public class Test : ITest
    {

    }
}";

}
