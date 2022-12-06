using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Singleton.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Singleton
{
    public class SingletonContentGenerator
    {
        internal static string GenerateClass(
            IGrouping<string, ClassDeclarationSyntax> group)
            => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {SingletonComponentsGenerator.GenerateDeclaration(group)}
    {{
	    {SingletonComponentsGenerator.GenerateInstanceField(group)}
        {SingletonComponentsGenerator.GenerateObjectToLock(group)}

        {SingletonComponentsGenerator.GenerateConstructor(group)}

        {SingletonComponentsGenerator.GenerateGetInstanceMethod(group)}
    }}
}}";
    }
}
