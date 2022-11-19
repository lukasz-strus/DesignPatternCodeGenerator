using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.CollectionHelper
{
    internal static class FilterCollectionHelper
    {
        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> FilterClassesByInterface(
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> classGroup,
            string interfaceName)
            => classGroup.SelectMany(x => x)
                    .Where(y => y.FirstAncestorOrSelf<TypeDeclarationSyntax>().BaseList.Types.ToString().Equals(interfaceName))
                    .GroupBy(z => z.Identifier.Text);


    }
}
