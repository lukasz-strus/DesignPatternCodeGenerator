using DesignPatternCodeGenerator.Analyzers;
using Xunit;
using Verifier = DesignPatternCodeGenerator.Tests.Verifiers.AnalyzerVerifier<
   DesignPatternCodeGenerator.Analyzers.DesignPatternAnalyzer>;

namespace DesignPatternCodeGenerator.Tests.Verifiers;

public class DesignPatternAnalyzerTests
{
    [Fact]
    public async Task Analyzer_ForFactoryChildClassWithoutInterface_ShouldThrowError()
    {
        var input =
@"using DesignPatternCodeGenerator.Attributes.Factory;
using System;

namespace DesignPatternCodeGenerator.Tests.Data
{
    [Factory]
    public interface ITest { }

    [FactoryChild]
    public class {|#0:Test1|} { }
}";

        var expectedError = Verifier.Diagnostic(DesingPatternDiagnosticsDescriptors.ClassMustImplementFactoryInterface.Id)
                                    .WithLocation(0)
                                    .WithArguments("Test1");

        await Verifier.VerifyAnalyzerAsync(input, expectedError);
    }
}
