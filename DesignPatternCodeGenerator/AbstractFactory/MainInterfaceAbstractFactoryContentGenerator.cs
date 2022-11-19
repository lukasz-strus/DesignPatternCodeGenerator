using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.AbstractFactory
{
    internal class MainInterfaceAbstractFactoryContentGenerator
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

    }
}
