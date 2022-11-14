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
    public class DesignPatternAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DesingPatternDiagnosticsDescriptors.ClassMustImplementFactoryInterface);

        public override void Initialize(AnalysisContext context)
        {

            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(CheckClassForInterfaces, SyntaxKind.ClassDeclaration);
        }

        private static void CheckClassForInterfaces(SyntaxNodeAnalysisContext context)
        {

//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif

            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
            var declaredSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);

            var attributeList = classDeclarationSyntax.AttributeLists.ToString();

            if (classDeclarationSyntax.BaseList is null
                && attributeList.Contains("[FactoryChild]"))
            {
                var error = Diagnostic.Create(DesingPatternDiagnosticsDescriptors.ClassMustImplementFactoryInterface,
                      classDeclarationSyntax.Identifier.GetLocation(),
                      declaredSymbol.Name);

                context.ReportDiagnostic(error);
            }

        }
    }
}
