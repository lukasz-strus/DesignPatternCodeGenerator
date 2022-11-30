using DesignPatternCodeGenerator.Base.Generators;
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
    {FacadeContentComponentGenerator.GenerateClassDeclaration(group)}
    {{
        {FacadeContentFieldsGenerator.GenerateFileds(group)}

        {FacadeContentMethodGenerator.GenerateMethod(group)}
    }}
}}";
    }
}
