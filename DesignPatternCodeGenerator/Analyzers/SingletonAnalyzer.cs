using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace DesignPatternCodeGenerator.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SingletonAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DesingPatternDiagnosticsDescriptors.SingletonMustBePartial);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeNamedType, SyntaxKind.ClassDeclaration);
        }

        private static void AnalyzeNamedType(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;
            var declaredSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
            var attributes = classDeclaration.AttributeLists.ToString();

            if (IsPartial(classDeclaration) || !IsSingleton(attributes))            
                return;
            
            var error = GetError(classDeclaration, declaredSymbol);
            context.ReportDiagnostic(error);
        }

        private static bool IsPartial(ClassDeclarationSyntax classDeclarationSyntax)
            => classDeclarationSyntax.Modifiers.Any(SyntaxKind.PartialKeyword);

        private static bool IsSingleton(string attributes) => attributes.Contains("Singleton");

        private static Diagnostic GetError(ClassDeclarationSyntax classDeclaration, INamedTypeSymbol symbol)
            => Diagnostic.Create(
                DesingPatternDiagnosticsDescriptors.SingletonMustBePartial,
                classDeclaration.Identifier.GetLocation(),
                symbol.Name);
    }
}
