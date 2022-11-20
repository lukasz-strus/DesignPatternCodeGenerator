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
            IGrouping<string, TypeDeclarationSyntax> interfaceGroup, 
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> factoryProductsGroups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(interfaceGroup) +
$@"
{{
    {BaseCodeGenerator.GenerateDeclaration(interfaceGroup, CodeType.Enum, true, false, true)}
    {{
	    {GenerateEnumElements(factoryProductsGroups)}
    }}
}}";
        private static string GenerateEnumElements(IEnumerable<IGrouping<string, TypeDeclarationSyntax>> factoryProductsGroups)
            => $"{string.Join("\n\t\t", factoryProductsGroups.Select(p => $"{p.Key},"))}\n";

    }
}
