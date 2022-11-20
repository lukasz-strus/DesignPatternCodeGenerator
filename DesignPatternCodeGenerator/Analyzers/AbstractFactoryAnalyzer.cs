using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DesignPatternCodeGenerator.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AbstractFactoryAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DesingPatternDiagnosticsDescriptors.ClassMustImplementAbstractFactoryInterface);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(CheckClassForInterfaces, SyntaxKind.ClassDeclaration);
        }

        private static void CheckClassForInterfaces(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;
            var declaredSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
            var attributes = classDeclaration.AttributeLists.ToString();

            if (IsNullBaseList(classDeclaration.BaseList) && IsAbstractFactoryChild(attributes))
            {
                var error = GetError(classDeclaration, declaredSymbol);

                context.ReportDiagnostic(error);
            }
        }

        private static bool IsAbstractFactoryChild(string attributes) => attributes.Contains("AbstractFactoryClass");

        private static bool IsNullBaseList(BaseListSyntax baseList) => baseList is null;

        private static Diagnostic GetError(ClassDeclarationSyntax classDeclaration, INamedTypeSymbol symbol)
            => Diagnostic.Create(
                    DesingPatternDiagnosticsDescriptors.ClassMustImplementAbstractFactoryInterface,
                    classDeclaration.Identifier.GetLocation(),
                    symbol.Name);
    }
}
