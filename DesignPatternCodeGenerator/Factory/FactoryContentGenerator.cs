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
            BaseCodeGenerator codeGenerator,
            IGrouping<string, InterfaceDeclarationSyntax> group)
            => codeGenerator.GenerateUsingsAndNamespace() +
$@"
{{
    {codeGenerator.GenerateDeclaration(CodeType.Interface)}
    {{
	    {FactoryContentComponentsGenerator.GenerateCreateMethodInterface(group)}
    }}
}}";

        internal static string GenerateClass
            (BaseCodeGenerator codeGenerator,
            IGrouping<string, InterfaceDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryChildGroups)
            => codeGenerator.GenerateUsingsAndNamespace() +
$@"
{{
    {codeGenerator.GenerateDeclaration(CodeType.Class)}
    {{
	    {FactoryContentComponentsGenerator.GenerateFields(group)}
        {FactoryContentComponentsGenerator.GenerateConstructor(group)}

	    {FactoryContentComponentsGenerator.GenerateCreateMethodClass(group, factoryChildGroups)}
    }}
}}";

    }
}
