using DesignPatternCodeGenerator.Base.Generators;
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
    public class {group.Key.Substring(1)}NullObject : {group.Key}
    {{
	    {NullObjectContentComponentGenerator.GenerateProperties(group)}

        {NullObjectContentComponentGenerator.GenerateMethods(group)}
    }}
}}";

    }
}
