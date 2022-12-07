using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Base.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.AbstractFactory.Compontents
{
    internal static class AbstractFactoryClassComponentsGenerator
    {
        internal static string GenerateDeclaration(
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup,
            IGrouping<string, ClassDeclarationSyntax> classGroup)
            => $"{BaseNamesGenerator.GetAccesibility(classGroup)} class {SyntaxHelper.GetAtributeValueText(classGroup)}Factory : " +
            $"I{SyntaxHelper.GetAtributeValueText(interfaceGroup)}Factory";

        internal static string GenerateCreateMethods(IGrouping<string, ClassDeclarationSyntax> group)
            => $"{string.Join("\n", group.Select(GenerateCreateMethod))}";

        private static string GenerateCreateMethod(ClassDeclarationSyntax classDeclarationSyntax)
            => $@"
        {GenerateCreateMethodDeclaration(classDeclarationSyntax)}
        {{
            {GenerateCreateMethodImplementation(classDeclarationSyntax)}
        }}";

        private static string GenerateCreateMethodDeclaration(ClassDeclarationSyntax classDeclarationSyntax)
            => $"public {GetTypeName(classDeclarationSyntax)} Create{GetTypeName(classDeclarationSyntax).Substring(1)}()";

        private static string GenerateCreateMethodImplementation(ClassDeclarationSyntax classDeclarationSyntax)
            => $"return new {classDeclarationSyntax.Identifier.Text}();";

        private static string GetTypeName(ClassDeclarationSyntax classDeclarationSyntax)
            => classDeclarationSyntax.BaseList.Types.ToString();
    }
}
