using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
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
    {BaseCodeGenerator.GenerateDeclaration(group, CodeType.Class, false, true)}
    {{
	    {SingletonContentComponentsGenerator.GenerateInstanceField(group)}
        {SingletonContentComponentsGenerator.GenerateObjectToLock(group)}

        {SingletonContentComponentsGenerator.GenerateConstructor(group)}

        {SingletonContentComponentsGenerator.GenerateGetInstanceMethod(group)}
    }}
}}";
    }
}
