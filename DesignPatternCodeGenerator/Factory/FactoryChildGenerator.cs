using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryChildGenerator
    {
        internal static string GenerateEnum(
            BaseCodeGenerator codeGenerator,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryChildGroups)
            => codeGenerator.GenerateUsingsAndNamespace() +
$@"
{{
    {codeGenerator.GenerateDeclaration(CodeType.Enum)}
    {{
	    {GenerateEnumElements(factoryChildGroups)}
    }}
}}";


        private static string GenerateEnumElements(IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryChildGroups)
            => $"{string.Join("\n\t\t", factoryChildGroups.Select(p => $"{p.Key},"))}\n";

    }
}
