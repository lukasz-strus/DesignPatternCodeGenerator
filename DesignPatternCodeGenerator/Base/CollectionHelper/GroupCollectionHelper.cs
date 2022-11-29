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
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> groups)
            => groups.SelectMany(x => x)
                     .GroupBy(z => z.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText);

        internal static IEnumerable<IGrouping<string, TypeDeclarationSyntax>> GroupByIdentifierText(
            IGrouping<string, TypeDeclarationSyntax> group)
            => group.ToList().GroupBy(x => x.Identifier.Text);

        internal static IEnumerable<IGrouping<string, MethodDeclarationSyntax>> GroupCollectionByAttributeValueText(
            IEnumerable<IGrouping<string, MethodDeclarationSyntax>> groups)
            => groups.SelectMany(x => x)
                     .OrderBy(y=>y.ReturnType.ToString())
                     .GroupBy(z => z.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText);
    }
}
