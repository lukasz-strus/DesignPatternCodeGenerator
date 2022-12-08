using DesignPatternCodeGenerator.AbstractFactory.Compontents;
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
            IGrouping<string, InterfaceDeclarationSyntax> mainInterfaceGroup,
            IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> interfaceGroups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(interfaceGroups.First()) +
$@"
{{
    {AbstractFactoryInterfaceComponentsGenerator.GenerateDeclaration(mainInterfaceGroup)}
    {{
	    {AbstractFactoryInterfaceComponentsGenerator.GenerateCreateMethods(interfaceGroups)}
    }}
}}";

        internal static string GenerateFactoryClass(
            IGrouping<string, InterfaceDeclarationSyntax> mainInterfaceGroup,
            IGrouping<string, ClassDeclarationSyntax> group)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(mainInterfaceGroup, group) +
$@"
{{
    {AbstractFactoryClassComponentsGenerator.GenerateDeclaration(mainInterfaceGroup, group)}
    {{
        {AbstractFactoryClassComponentsGenerator.GenerateCreateMethods(group)}
    }}
}}";
    }
}
