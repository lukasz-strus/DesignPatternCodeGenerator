using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryChildEnumGenerator
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
