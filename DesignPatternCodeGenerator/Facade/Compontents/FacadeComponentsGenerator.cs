using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Facade.Compontents
{
    internal static class FacadeComponentsGenerator
    {
        internal static string GenerateClassDeclaration(IGrouping<string, MethodDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} class {BaseNamesGenerator.GetClassName(group)}Facade";

        internal static string GetClassName(ClassDeclarationSyntax classDeclaration)
            => classDeclaration.Identifier.Text;

        internal static string GetFacadeFieldName(ClassDeclarationSyntax classDeclaration)
             => GetClassName(classDeclaration).Substring(0, 1).ToLower() + GetClassName(classDeclaration).Remove(0, 1);

    }
}
