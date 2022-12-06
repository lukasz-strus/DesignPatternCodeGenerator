using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory
{
    public static class FactoryInterfaceContentComponentGenerator
    {
        internal static string GenerateDeclaration(IGrouping<string, InterfaceDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} interface {BaseNamesGenerator.GetInterfaceName(group, GeneratorAttributeType.Factory)}";

        internal static string GenerateCreateMethod(IGrouping<string, InterfaceDeclarationSyntax> group)
            => $"{string.Join("\n", group.Select(FactoryContentComponentsGenerator.GenerateCreateMethodDeclaration).Select(x => x + ";"))}";
    }
}
