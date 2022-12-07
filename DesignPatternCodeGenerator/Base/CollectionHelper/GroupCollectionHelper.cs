using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.CollectionHelper
{
    internal static class GroupCollectionHelper
    {
        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GroupCollectionByAttributeValueText(
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> groups)
            => groups.SelectMany(x => x)
                     .GroupBy(z => z.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText);

        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GroupByIdentifierText(
            IGrouping<string, ClassDeclarationSyntax> group)
            => group.ToList().GroupBy(x => x.Identifier.Text);

        internal static IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> GroupCollectionByAttributeValueText(
            IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> groups)
            => groups.SelectMany(x => x)
             .GroupBy(z => z.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText);

        internal static IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> GroupByIdentifierText(
            IGrouping<string, InterfaceDeclarationSyntax> group)
            => group.ToList().GroupBy(x => x.Identifier.Text);

        internal static IEnumerable<IGrouping<string, MethodDeclarationSyntax>> GroupCollectionByAttributeValueText(
            IEnumerable<IGrouping<string, MethodDeclarationSyntax>> groups)
            => groups.SelectMany(x => x)
                     .OrderBy(y => y.ReturnType.ToString())
                     .GroupBy(z => z.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText);
    }
}
