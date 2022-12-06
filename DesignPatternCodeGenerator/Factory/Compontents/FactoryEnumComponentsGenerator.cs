using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory.Compontents
{
    internal static class FactoryEnumComponentsGenerator
    {
        internal static string GenerateDeclaration(IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup)
            => $"{BaseNamesGenerator.GetAccesibility(interfaceGroup)} enum {BaseNamesGenerator.GetClassName(interfaceGroup)}FactoryType";

        internal static string GenerateEnumElements(IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
            => $"{string.Join("\n\t\t", factoryProductsGroups.Select(p => $"{p.Key},"))}\n";
    }
}
