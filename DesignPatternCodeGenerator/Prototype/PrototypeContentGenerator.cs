using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace DesignPatternCodeGenerator.Prototype
{
    internal static class PrototypeContentGenerator
    {
        internal static string GenerateClass(IGrouping<string, ClassDeclarationSyntax> group)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {BaseCodeGenerator.GenerateDeclaration(group, CodeType.Class, false, true)}
    {{
        {PrototypeContentComponentsGenerator.GenerateShallowClone(group)}

        {PrototypeContentComponentsGenerator.GenerateDeepClone(group)}
    }}
}}
";


    }
}
