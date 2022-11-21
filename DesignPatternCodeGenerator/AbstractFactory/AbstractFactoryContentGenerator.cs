using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
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
	    {AbstractFactoryContentComponentGenerator.GenerateCreateMethodInterface(groups)}
    }}
}}";

        internal static string GenerateFactoryClass(
            IGrouping<string, TypeDeclarationSyntax> mainInterfaceGroup,
            IGrouping<string, TypeDeclarationSyntax> group)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(mainInterfaceGroup, group) +
$@"
{{
    {AbstractFactoryContentComponentGenerator.GenerateClassDeclaration(mainInterfaceGroup, group)}
    {{
        {AbstractFactoryContentComponentGenerator.GenerateCreateMethods(group)}
    }}
}}";
    }
}
