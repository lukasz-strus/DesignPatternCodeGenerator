using DesignPatternCodeGenerator.Analyzers;
using DesignPatternCodeGenerator.Attributes.Prototype;
using Xunit;
using Verifier = DesignPatternCodeGenerator.Tests.Verifiers.AnalyzerVerifier<
   DesignPatternCodeGenerator.Analyzers.PrototypePartialAnalyzer>;

namespace DesignPatternCodeGenerator.Tests.Verifiers;

public class PrototypePartialAnalyzerTests
{
    [Fact]
    public async Task Analyzer_ForNoPartialPrototypClass_ShouldThrowError()
    {
        var input =
@"using DesignPatternCodeGenerator.Attributes.Prototype;

namespace Test.Test
{
    [Prototype]
    public class {|#0:Test1|} { }
}";

        var expectedError = Verifier.Diagnostic(DesingPatternDiagnosticsDescriptors.PrototypeMustBePartial.Id)
                                    .WithLocation(0)
                                    .WithArguments("Test1");

        await Verifier.VerifyAnalyzerAsync(input, typeof(PrototypeAttribute), expectedError);
    }
}
