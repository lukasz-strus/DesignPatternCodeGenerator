using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Facade.Compontents;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Facade
{
    internal static class FacadeContentGenerator
    {
        internal static string GenerateClass(IGrouping<string, MethodDeclarationSyntax> group)
        => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {FacadeComponentsGenerator.GenerateClassDeclaration(group)}
    {{
        {FacadeFieldsComponentsGenerator.GenerateFileds(group)}

        {FacadeMethodComponentsGenerator.GenerateMethod(group)}
    }}
}}";
    }
}
