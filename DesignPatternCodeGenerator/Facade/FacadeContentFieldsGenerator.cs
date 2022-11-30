using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Facade
{
    internal static class FacadeContentFieldsGenerator
    {
        internal static string GenerateFileds(IGrouping<string, MethodDeclarationSyntax> group)
            => $"{string.Join("\n\t\t", GetClassDeclarations(group).Select(GenerateField))}";

        private static IEnumerable<ClassDeclarationSyntax> GetClassDeclarations(IGrouping<string, MethodDeclarationSyntax> group)
            => group.Select(x => x.Parent).OfType<ClassDeclarationSyntax>().Distinct();

        private static string GenerateField(ClassDeclarationSyntax classDeclaration)
            => GenerateFieldDeclaration(classDeclaration) + " = " + GenerateFieldInitialization(classDeclaration);

        private static string GenerateFieldDeclaration(ClassDeclarationSyntax classDeclaration)
            => $"private {FacadeContentComponentGenerator.GetClassName(classDeclaration)} " +
            $"_{FacadeContentComponentGenerator.GetFacadeFieldName(classDeclaration)}";

        private static string GenerateFieldInitialization(ClassDeclarationSyntax classDeclaration)
            => $"new {FacadeContentComponentGenerator.GetClassName(classDeclaration)}();";
    }
}
