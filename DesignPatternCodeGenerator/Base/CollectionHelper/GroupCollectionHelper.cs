using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.CollectionHelper
{
    internal static class GroupCollectionHelper
    {
        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GroupByAttribute(
            this IEnumerable<IGrouping<string, ClassDeclarationSyntax>> groups)
            => groups.SelectMany(x => x)
                     .GroupBy(GetAtribute);

        internal static IEnumerable<IGrouping<string, MethodDeclarationSyntax>> GroupByAttribute(
            this IEnumerable<IGrouping<string, MethodDeclarationSyntax>> groups)
            => groups.SelectMany(x => x)
                     .OrderBy(y => y.ReturnType.ToString())
                     .GroupBy(GetAtribute);

        internal static IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> GroupByAttribute(
            this IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> groups)
            => groups.SelectMany(x => x)
                     .GroupBy(GetAtribute);

        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GroupByIdentifier(
            this IGrouping<string, ClassDeclarationSyntax> group)
            => group.ToList().GroupBy(x => x.Identifier.Text);

        internal static IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> GroupByIdentifier(
            this IGrouping<string, InterfaceDeclarationSyntax> group)
            => group.ToList().GroupBy(x => x.Identifier.Text);

        private static string GetAtribute(MemberDeclarationSyntax member)
            => member.AttributeLists.First().Attributes
                                    .First().ArgumentList.Arguments
                                    .First().Expression
                                    .GetFirstToken().ValueText;
    }
}
