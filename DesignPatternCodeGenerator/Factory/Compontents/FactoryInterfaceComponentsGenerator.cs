using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory.Compontents
{
    internal static class FactoryInterfaceComponentsGenerator
    {
        internal static string GenerateDeclaration(IGrouping<string, InterfaceDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} interface {BaseNamesGenerator.GetInterfaceName(group, GeneratorAttributeType.Factory)}";

        internal static string GenerateCreateMethod(IGrouping<string, InterfaceDeclarationSyntax> group)
            => $"{string.Join("\n", group.Select(FactoryComponentsGenerator.GenerateCreateMethodDeclaration).Select(x => x + ";"))}";
    }
}
