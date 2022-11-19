using DesignPatternCodeGenerator.Analyzers;
using DesignPatternCodeGenerator.Attributes.Factory;
using Xunit;
using Verifier = DesignPatternCodeGenerator.Tests.Verifiers.AnalyzerVerifier<
   DesignPatternCodeGenerator.Analyzers.FactoryAnalyzer>;

namespace DesignPatternCodeGenerator.Tests.Verifiers;

public class FactoryAnalyzerTests
{
    [Fact]
    public async Task Analyzer_ForFactoryChildClassWithoutInterface_ShouldThrowError()
    {
        var input =
@"using DesignPatternCodeGenerator.Attributes.Factory;
using System;

namespace Test.Test
{
    [Factory]
    public interface ITest { }

    [FactoryProduct]
    public class {|#0:Test1|} { }
}";

        var expectedError = Verifier.Diagnostic(DesingPatternDiagnosticsDescriptors.ClassMustImplementFactoryInterface.Id)
                                    .WithLocation(0)
                                    .WithArguments("Test1");

        await Verifier.VerifyAnalyzerAsync(input, typeof(FactoryProductAttribute), expectedError);
    }
}
