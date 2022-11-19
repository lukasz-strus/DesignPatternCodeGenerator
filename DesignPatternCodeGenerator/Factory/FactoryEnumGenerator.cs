using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryEnumGenerator
    {
        internal static string GenerateEnum(
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup, 
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(interfaceGroup) +
$@"
{{
    {BaseCodeGenerator.GenerateDeclaration(interfaceGroup, CodeType.Enum, true, false, true)}
    {{
	    {GenerateEnumElements(factoryProductsGroups)}
    }}
}}";
        private static string GenerateEnumElements(IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
            => $"{string.Join("\n\t\t", factoryProductsGroups.Select(p => $"{p.Key},"))}\n";

    }
}
