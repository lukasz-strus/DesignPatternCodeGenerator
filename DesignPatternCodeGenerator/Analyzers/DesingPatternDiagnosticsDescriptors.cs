using Microsoft.CodeAnalysis;

namespace DesignPatternCodeGenerator.Analyzers
{
    public class DesingPatternDiagnosticsDescriptors
    {
        public static readonly DiagnosticDescriptor ClassMustImplementFactoryInterface
            = new DiagnosticDescriptor("DES0001",
                         "Class must implement factory interface",
                         "The class '{0}' implement factory interface",
                         "DesingPatternAnalyzer",
                         DiagnosticSeverity.Error,
                         true);
    }
}
