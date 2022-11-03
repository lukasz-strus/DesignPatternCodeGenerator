using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Collections.Generic;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryChildGenerator
    {
        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> FilterFactoryChild(
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> classGroup,
            string interfaceName) => 
            classGroup
                .SelectMany(x => x)
                .Where(y => y.FirstAncestorOrSelf<TypeDeclarationSyntax>().BaseList.Types.ToString().Contains(interfaceName))
                .GroupBy(z => z.Identifier.Text);
        
    }
}
