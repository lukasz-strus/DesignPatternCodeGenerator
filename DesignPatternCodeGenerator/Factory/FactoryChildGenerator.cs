using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using DesignPatternCodeGenerator.Base.Helpers;
using System.Collections.Generic;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryChildGenerator
    {
        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> FilterFactoryChild(
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> classGroup,
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup)
        {
            return classGroup.Select(x => x.FilterElements(y => y.Identifier.Text == interfaceGroup.Key));
        }
    }
}
