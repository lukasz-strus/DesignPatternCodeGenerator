using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace DesignPatternCodeGenerator.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PrototypeParameterlessCtorAnalyzer : DiagnosticAnalyzer
    {
        //TODO zmienić contains na przyrównanie typów
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DesingPatternDiagnosticsDescriptors.ClassMustHaveParameterlessConstructor);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeMembers, SyntaxKind.ClassDeclaration);
        }

        private static void AnalyzeMembers(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;

            var attributes = classDeclaration.AttributeLists.ToString();

            if (!IsPrototype(attributes))
                return;

            var properties = classDeclaration.Members.OfType<PropertyDeclarationSyntax>();

            foreach (var property in properties)
            {
                var declaredSymbol = context.SemanticModel.GetDeclaredSymbol(property);

                var proptClassDeclaration = GetClassDeclaration(context, property);
                if (proptClassDeclaration is null)
                    continue;

                var ctors = proptClassDeclaration.Members.OfType<ConstructorDeclarationSyntax>();
                if (!ctors.Any())
                    continue;

                var parameterlessCtor = ctors.Where(x => !x.ParameterList.Parameters.Any());
                if (parameterlessCtor.Any())
                    continue;

                var error = GetError(property, declaredSymbol);
                context.ReportDiagnostic(error);
            }
        }

        private static ClassDeclarationSyntax GetClassDeclaration(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax property)
        {
            var root = (CompilationUnitSyntax)context.SemanticModel.SyntaxTree.GetRoot();

            return root.DescendantNodes()
                       .OfType<ClassDeclarationSyntax>()
                       .Where(x => IsClassType(x, property.Type.ToString()))
                       .FirstOrDefault();
        }

        private static bool IsPrototype(string attributes) => attributes.Contains("Prototype");

        private static Diagnostic GetError(PropertyDeclarationSyntax propertyDeclarationSyntax, IPropertySymbol symbol)
            => Diagnostic.Create(
                DesingPatternDiagnosticsDescriptors.ClassMustHaveParameterlessConstructor,
                propertyDeclarationSyntax.Identifier.GetLocation(),
                symbol.Name);

        private static bool IsClassType(ClassDeclarationSyntax classSyntax, string typeName)
        {
            var nullableType = typeName.EndsWith("?") ? typeName.Remove(typeName.Length - 1) : typeName;

            return classSyntax.Identifier.Text.Contains(nullableType);
        }
    }
}
