using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Diagnostics;
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
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
            var declaredSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
            var attributeList = classDeclarationSyntax.AttributeLists.ToString();

            if (!IsPartial(classDeclarationSyntax)
                && attributeList.Contains("[Singleton]"))
            {
                var error = Diagnostic.Create(DesingPatternDiagnosticsDescriptors.SingletonMustBePartial,
                      classDeclarationSyntax.Identifier.GetLocation(),
                      declaredSymbol.Name);

                context.ReportDiagnostic(error);
            }

        }

        private static bool IsPartial(ClassDeclarationSyntax classDeclarationSyntax)
            => classDeclarationSyntax.Modifiers.Any(SyntaxKind.PartialKeyword);
    }
}
