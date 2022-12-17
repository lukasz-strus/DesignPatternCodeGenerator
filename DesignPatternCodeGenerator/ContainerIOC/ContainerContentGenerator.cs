using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.ContainerIOC.Components;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.ContainerIOC
{
    internal static class ContainerContentGenerator
    {
        internal static string GenerateClass(
            IGrouping<string, ClassDeclarationSyntax> group,
            GeneratorExecutionContext context)
            => BaseCodeGenerator.GenerateContainerUsingsAndNamespace(group) +
$@"
{{
    {ContainerClassComponentsGenerator.GenerateDeclaration(group)}
    {{
        {ContainerClassComponentsGenerator.GenerateMethod(group, context)}
    }}
}}";

    }
}
