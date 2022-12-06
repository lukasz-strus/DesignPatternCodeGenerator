using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
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
    {FactoryInterfaceContentComponentGenerator.GenerateDeclaration(group)}
    {{
	    {FactoryInterfaceContentComponentGenerator.GenerateCreateMethod(group)}
    }}
}}";

        internal static string GenerateClass(
            IGrouping<string, InterfaceDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {FactoryClassContentComponentGenerator.GenerateDeclaration(group)}
    {{
	    {FactoryClassContentComponentGenerator.GenerateFields(group)}
        {FactoryClassContentComponentGenerator.GenerateConstructor(group)}

	    {FactoryClassContentComponentGenerator.GenerateCreateMethodClass(group, factoryProductsGroups)}
    }}
}}";

    }
}
