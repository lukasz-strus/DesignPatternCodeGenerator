using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.NullObject.Compontents;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.NullObject
{
    public static class NullObjectContentGenerator
    {
        internal static string GenerateClass(
            IGrouping<string, InterfaceDeclarationSyntax> group)
        => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {NullObjectComponentsGenerator.GenerateDeclaration(group)}
    {{
	    {NullObjectComponentsGenerator.GenerateProperties(group)}

        {NullObjectComponentsGenerator.GenerateMethods(group)}
    }}
}}";



    }
}
