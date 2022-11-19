using DesignPatternCodeGenerator.Analyzers;
using DesignPatternCodeGenerator.Attributes.Factory;
using Xunit;
using Verifier = DesignPatternCodeGenerator.Tests.Verifiers.AnalyzerVerifier<
   DesignPatternCodeGenerator.Analyzers.SingletonAnalyzer>;

namespace DesignPatternCodeGenerator.Tests.Verifiers
{
    public class SingletonAnalyzerTests
    {
        [Fact]
        public async Task Analyzer_ForFactoryChildClassWithoutInterface_ShouldThrowError()
        {
            var input =
    @"using DesignPatternCodeGenerator.Attributes.Singleton;

namespace Test.Test
{
    [Singleton]
    public class {|#0:Test1|} { }
}";



            var expectedError = Verifier.Diagnostic(DesingPatternDiagnosticsDescriptors.SingletonMustBePartial.Id)
                                        .WithLocation(0)
                                        .WithArguments("Test1");

            await Verifier.VerifyAnalyzerAsync(input, typeof(FactoryProductAttribute), expectedError);
        }
    }
}
