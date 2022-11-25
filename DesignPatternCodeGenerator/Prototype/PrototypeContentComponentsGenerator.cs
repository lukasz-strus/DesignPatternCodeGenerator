using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace DesignPatternCodeGenerator.Prototype
{
    internal static class PrototypeContentComponentsGenerator
    {
        internal static string GenerateDeepClone(IGrouping<string, ClassDeclarationSyntax> group)
            => $@"{GenerateMethodDeclaration(group, CopyType.Deep)}
        {{
            {GenerateShallowMethod(group)}

            {GenerateObjectFieldsClone(group)}
        }}";

        private static object GenerateObjectFieldsClone(IGrouping<string, ClassDeclarationSyntax> group)
        {
            return $"clone.Address = new Address(){GenerateNewObject(group)}";
        } 

        private static object GenerateNewObject(IGrouping<string, ClassDeclarationSyntax> group)
            => "";

        internal static string GenerateShallowClone(IGrouping<string, ClassDeclarationSyntax> group)
            =>$@"{GenerateMethodDeclaration(group, CopyType.Shallow)}
        {{
            {GenerateShallowMethod(group)}
        }}";

        private static string GenerateShallowMethod(IGrouping<string, ClassDeclarationSyntax> group)
            => $"return ({group.Key})this.MemberwiseClone();";

        private static string GenerateMethodDeclaration(IGrouping<string, ClassDeclarationSyntax> group, CopyType type)
            => $"public {group.Key} {type}Copy()";
    }
}
