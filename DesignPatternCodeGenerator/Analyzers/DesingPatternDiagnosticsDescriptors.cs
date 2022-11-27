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

        public static readonly DiagnosticDescriptor ClassMustImplementAbstractFactoryInterface
           = new DiagnosticDescriptor(
                "DES0003",
                "Class must implement abstract factory interface",
                "The class '{0}' implement abstract factory interface",
                "DesingPatternAnalyzer",
                DiagnosticSeverity.Error,
                true);

        public static readonly DiagnosticDescriptor PrototypeMustBePartial
           = new DiagnosticDescriptor(
               "DES0004",
               "Prototype class must be partial",
               "The prototype class '{0}' must be partial",
               "DesingPatternAnalyzer",
               DiagnosticSeverity.Error,
               true);
    }
}
