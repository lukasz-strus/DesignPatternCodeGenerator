using DesignPatternCodeGenerator.Analyzers;
using DesignPatternCodeGenerator.Attributes.Prototype;
using Xunit;
using Verifier = DesignPatternCodeGenerator.Tests.Verifiers.AnalyzerVerifier<
   DesignPatternCodeGenerator.Analyzers.PrototypeParameterlessCtorAnalyzer>;

namespace DesignPatternCodeGenerator.Tests.Verifiers;

public class PrototypeNoParameterlessCtorAnalyzerTests
{
    [Fact]
    public async Task Analyzer_ForNoPartialPrototypClass_ShouldThrowError()
    {
        var input =
@"using DesignPatternCodeGenerator.Attributes.Prototype;

namespace Test.Test
{
    [Prototype]
    public class Test
    {
        public Property? {|#0:TestProperty|} {get; set;}
    }

    public class Property
    {
        public string TestString {get; set;}
        
        public Property(string testString)
        {
            TestString = testString;
        }
    }    
}";

        var expectedError = Verifier.Diagnostic(DesingPatternDiagnosticsDescriptors.ClassMustHaveParameterlessConstructor.Id)
                                    .WithLocation(0)
                                    .WithArguments("TestProperty");

        await Verifier.VerifyAnalyzerAsync(input, typeof(PrototypeAttribute), expectedError);
    }
}
