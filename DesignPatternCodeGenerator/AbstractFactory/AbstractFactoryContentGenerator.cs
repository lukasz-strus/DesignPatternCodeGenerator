using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Base.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.AbstractFactory
{
    internal class AbstractFactoryContentGenerator
    {
        internal static string GenerateMainInterface(
            IGrouping<string, TypeDeclarationSyntax> mainInterfaceGroup,
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> groups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(groups.First()) +
$@"
{{
    {BaseCodeGenerator.GenerateDeclaration(mainInterfaceGroup, CodeType.Interface, true, false, true)}
    {{
	    {GenerateCreateMethodInterface(groups)}
    }}
}}";

        private static string GenerateCreateMethodInterface(IEnumerable<IGrouping<string, TypeDeclarationSyntax>> groups)
        {
            return $"{string.Join("\n\t\t", groups.Select(x => $"{x.Key} Create{x.Key.Substring(1)}();"))}";
        }

        internal static string GenerateFactoryClass(
            IGrouping<string, TypeDeclarationSyntax> mainInterfaceGroup,
            IGrouping<string, TypeDeclarationSyntax> group)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(mainInterfaceGroup) +
$@"
{{
    {GenerateDeclaration(mainInterfaceGroup, group)}
    {{
        {GenerateCreateMethods(group)}
    }}
}}";

        private static string GenerateDeclaration(
            IGrouping<string, TypeDeclarationSyntax> mainInterfaceGroup,
            IGrouping<string, TypeDeclarationSyntax> filtredClassGroup)
            => $"public partial class {SyntaxHelper.GetAtributeValueText(filtredClassGroup)}Factory : " +
            $"I{SyntaxHelper.GetAtributeValueText(mainInterfaceGroup)}Factory";


        private static string GenerateCreateMethods(IGrouping<string, TypeDeclarationSyntax> group)
        {
            return $"{string.Join("\n", group.Select(x => GenerateCreateMethod(x, "test")))}";
        }

        private static string GenerateCreateMethod(TypeDeclarationSyntax syntax, string typeName)
        {
            return $@"
        public {GetTypeName(syntax)} Create{GetTypeName(syntax).Substring(1)}()
        {{
            return new {syntax.Identifier.Text}();
        }}";
        }

        private static string GetTypeName(TypeDeclarationSyntax syntax)
            => syntax.BaseList.Types.ToString();


    }
}
