using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Base.CollectionHelper
{
    internal static class GroupCollectionHelper
    {
        internal static IEnumerable<IGrouping<string, TypeDeclarationSyntax>> GroupCollectionByAttributeValueText(
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> interfaceGroups)
            => interfaceGroups.SelectMany(x => x)
                     .GroupBy(z => z.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText);

        internal static IEnumerable<IGrouping<string, TypeDeclarationSyntax>> GroupByIdentifierText(
            IGrouping<string, TypeDeclarationSyntax> interfaceGroup)
            => interfaceGroup.ToList().GroupBy(x => x.Identifier.Text);
    }
}
