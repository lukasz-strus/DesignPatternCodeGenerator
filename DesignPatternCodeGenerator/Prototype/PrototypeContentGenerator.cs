using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
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
    {BaseCodeGenerator.GenerateDeclaration(group, CodeType.Class, false, true)}
    {{
        {PrototypeContentComponentsGenerator.GenerateShallowClone(group)}

        {PrototypeContentComponentsGenerator.GenerateDeepClone(group, allClassGroups)}
    }}
}}
";
    }
}
