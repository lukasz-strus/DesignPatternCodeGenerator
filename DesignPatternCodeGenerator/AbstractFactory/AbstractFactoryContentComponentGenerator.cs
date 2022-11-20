using DesignPatternCodeGenerator.Base.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.AbstractFactory
{
    internal static class AbstractFactoryContentComponentGenerator
    {
        internal static string GenerateCreateMethodInterface(IEnumerable<IGrouping<string, TypeDeclarationSyntax>> groups)
            => $"{string.Join("\n\t\t", groups.Select(x => $"{x.Key} Create{x.Key.Substring(1)}();"))}";

        internal static string GenerateClassDeclaration(
            IGrouping<string, TypeDeclarationSyntax> mainInterfaceGroup,
            IGrouping<string, TypeDeclarationSyntax> filtredClassGroup)
            => $"public class {SyntaxHelper.GetAtributeValueText(filtredClassGroup)}Factory : " +
            $"I{SyntaxHelper.GetAtributeValueText(mainInterfaceGroup)}Factory";


        internal static string GenerateCreateMethods(IGrouping<string, TypeDeclarationSyntax> group)
            => $"{string.Join("\n", group.Select(x => GenerateCreateMethod(x)))}";

        private static string GenerateCreateMethod(TypeDeclarationSyntax syntax)
        => $@"
        {GenerateCreateMethodDeclaration(syntax)}
        {{
            {GenerateCreateMethodImplementation(syntax)}
        }}";

        private static string GenerateCreateMethodDeclaration(TypeDeclarationSyntax syntax)
            => $"public {GetTypeName(syntax)} Create{GetTypeName(syntax).Substring(1)}()";

        private static string GenerateCreateMethodImplementation(TypeDeclarationSyntax syntax)
            => $"return new {syntax.Identifier.Text}();";

        private static string GetTypeName(TypeDeclarationSyntax syntax)
            => syntax.BaseList.Types.ToString();
    }
}
