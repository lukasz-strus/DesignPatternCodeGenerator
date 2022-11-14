using Microsoft.CodeAnalysis;

namespace DesignPatternCodeGenerator.Analyzers
{
    public class DesingPatternDiagnosticsDescriptors
    {
        public static readonly DiagnosticDescriptor ClassMustImplementFactoryInterface
            = new DiagnosticDescriptor(
                "DES0001",
                "Class must implement factory interface",
                "The class '{0}' implement factory interface",
                "DesingPatternAnalyzer",
                DiagnosticSeverity.Error,
                true);

        public static readonly DiagnosticDescriptor SingletonMustBePartial
           = new DiagnosticDescriptor(
               "DES0002",
               "Singleton class must be partial",
               "The singleton class '{0}' must be partial",
               "DesingPatternAnalyzer",
               DiagnosticSeverity.Error,
               true);
    }
}
