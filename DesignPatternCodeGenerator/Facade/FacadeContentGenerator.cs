using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DesignPatternCodeGenerator.Facade
{
    internal static class FacadeContentGenerator
    {
        internal static string GenerateClass(IGrouping<string, MethodDeclarationSyntax> group)
        => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {FacadeContentComponentGenerator.GenerateClassDeclaration(group)}
    {{
        {FacadeContentComponentGenerator.GenerateFileds(group)}

        {FacadeContentComponentGenerator.GenerateMethod(group)}
    }}
}}";

    }
}
