using DesignPatternCodeGenerator.Analyzers;
using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using DesignPatternCodeGenerator.Attributes.Factory;
using Xunit;
using Verifier = DesignPatternCodeGenerator.Tests.Verifiers.AnalyzerVerifier<
   DesignPatternCodeGenerator.Analyzers.AbstractFactoryAnalyzer>;

namespace DesignPatternCodeGenerator.Tests.Verifiers;

public class AbstractFactoryAnalyzerTests
{
    [Fact]
    public async Task Analyzer_ForFactoryChildClassWithoutInterface_ShouldThrowError()
    {
        var input =
@"using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;

namespace Test.Test
{
    [AbstractFactory(""UIElement"")]
    public interface IButton
    {
        void Render();
        void HandleClick();
    }

    [AbstractFactoryChild(""Windows"")]
    public class {|#0:WindowsButton|}
    {
        public void HandleClick()
        {
            Console.WriteLine(""Handle windows click event"");
        }

        public void Render()
        {
            Console.WriteLine(""Render windows button"");
        }
    }
}";

        var expectedError = Verifier.Diagnostic(DesingPatternDiagnosticsDescriptors.ClassMustImplementAbstractFactoryInterface.Id)
                                    .WithLocation(0)
                                    .WithArguments("WindowsButton");

        await Verifier.VerifyAnalyzerAsync(input, typeof(AbstractFactoryChildAttribute), expectedError);
    }
}
