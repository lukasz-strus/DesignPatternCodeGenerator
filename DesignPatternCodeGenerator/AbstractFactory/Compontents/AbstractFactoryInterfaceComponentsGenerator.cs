using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.AbstractFactory.Compontents
{
    internal static class AbstractFactoryInterfaceComponentsGenerator
    {
        internal static string GenerateDeclaration(IGrouping<string, InterfaceDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} interface {BaseNamesGenerator.GetInterfaceName(group, GeneratorAttributeType.Factory)}";

        internal static string GenerateCreateMethods(IEnumerable<IGrouping<string, TypeDeclarationSyntax>> groups)
            => $"{string.Join("\n\t\t", groups.Select(x => $"{x.Key} Create{x.Key.Substring(1)}();"))}";
    }
}
