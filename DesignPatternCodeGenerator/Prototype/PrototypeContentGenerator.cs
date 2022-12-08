using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Prototype.Compontents;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Prototype
{
    internal static class PrototypeContentGenerator
    {
        internal static string GenerateClass(
            IGrouping<string, ClassDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> allClassGroups)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {PrototypeComponentsGenerator.GenerateDeclaration(group)}
    {{
        {PrototypeComponentsGenerator.GenerateShallowClone(group)}

        {PrototypeComponentsGenerator.GenerateDeepClone(group, allClassGroups)}
    }}
}}
";
    }
}
