using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Facade
{
    internal static class FacadeContentComponentGenerator
    {
        internal static string GenerateClassDeclaration(IGrouping<string, MethodDeclarationSyntax> group)
            => $"public class {group.Key}Facade";

        internal static string GetClassName(ClassDeclarationSyntax classDeclaration)
            => classDeclaration.Identifier.Text;

        internal static string GetFacadeFieldName(ClassDeclarationSyntax classDeclaration)
             => GetClassName(classDeclaration).Substring(0, 1).ToLower() + GetClassName(classDeclaration).Remove(0, 1);

    }
}
