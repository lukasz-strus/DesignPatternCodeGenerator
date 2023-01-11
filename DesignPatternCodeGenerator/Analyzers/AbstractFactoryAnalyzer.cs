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
        //TODO pobrać listę interfejsów i sprawdzić czy któryś jest oznaczony atrybutem AbstractFactory
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

            if (!IsNullBaseList(classDeclaration.BaseList) || !IsAbstractFactoryProduct(attributes))            
                return;
            

            var error = GetError(classDeclaration, declaredSymbol);

            context.ReportDiagnostic(error);
        }

        private static bool IsAbstractFactoryProduct(string attributes) => attributes.Contains("AbstractFactoryClass");

        private static bool IsNullBaseList(BaseListSyntax baseList) => baseList is null;

        private static Diagnostic GetError(ClassDeclarationSyntax classDeclaration, INamedTypeSymbol symbol)
            => Diagnostic.Create(
                DesingPatternDiagnosticsDescriptors.ClassMustImplementAbstractFactoryInterface,
                classDeclaration.Identifier.GetLocation(),
                symbol.Name);
    }
}
