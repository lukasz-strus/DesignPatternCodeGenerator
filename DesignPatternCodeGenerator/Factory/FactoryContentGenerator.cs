using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Factory.Compontents;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryContentGenerator
    {
        internal static string GenerateInterface(
            IGrouping<string, InterfaceDeclarationSyntax> group)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {FactoryInterfaceComponentsGenerator.GenerateDeclaration(group)}
    {{
	    {FactoryInterfaceComponentsGenerator.GenerateCreateMethod(group)}
    }}
}}";

        internal static string GenerateClass(
            IGrouping<string, InterfaceDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {FactoryClassComponentsGenerator.GenerateDeclaration(group)}
    {{
	    {FactoryClassComponentsGenerator.GenerateFields(group)}
        {FactoryClassComponentsGenerator.GenerateConstructor(group)}

	    {FactoryClassComponentsGenerator.GenerateCreateMethodClass(group, factoryProductsGroups)}
    }}
}}";

        internal static string GenerateEnum(
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(interfaceGroup) +
$@"
{{
    {FactoryEnumComponentsGenerator.GenerateDeclaration(interfaceGroup)}
    {{
	    {FactoryEnumComponentsGenerator.GenerateEnumElements(factoryProductsGroups)}
    }}
}}";

    }
}
