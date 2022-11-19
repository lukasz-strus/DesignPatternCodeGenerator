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
    {BaseCodeGenerator.GenerateDeclaration(group, CodeType.Interface, true, false, true)}
    {{
	    {FactoryContentComponentsGenerator.GenerateCreateMethodInterface(group)}
    }}
}}";

        internal static string GenerateClass
            (IGrouping<string, InterfaceDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {BaseCodeGenerator.GenerateDeclaration(group, CodeType.Class, true, false, true)}
    {{
	    {FactoryContentComponentsGenerator.GenerateFields(group)}
        {FactoryContentComponentsGenerator.GenerateConstructor(group)}

	    {FactoryContentComponentsGenerator.GenerateCreateMethodClass(group, factoryProductsGroups)}
    }}
}}";

    }
}
