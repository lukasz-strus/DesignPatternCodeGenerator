using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.CollectionHelper
{
    internal static class FilterCollectionHelper
    {
        internal static IEnumerable<IGrouping<string, TypeDeclarationSyntax>> FilterClassesByInterface(
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> classGroup,
            string interfaceName)
            => classGroup.SelectMany(x => x)
                    .Where(y => y.FirstAncestorOrSelf<TypeDeclarationSyntax>().BaseList.Types.ToString().Equals(interfaceName))
                    .GroupBy(z => z.Identifier.Text);

        internal static IEnumerable<IGrouping<string, TypeDeclarationSyntax>> FilterClassesByInterface(
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> classGroup,
            IEnumerable<string> interfaceName)
        {
            var ret = new List<IGrouping<string, TypeDeclarationSyntax>>();

            interfaceName.ToList().ForEach(
                x =>
                {
                    ret.AddRange(FilterClassesByInterface(classGroup, x));
                });

            return ret.Distinct();
        }

        internal static IEnumerable<IGrouping<string, TypeDeclarationSyntax>> FilterClassesByAttributeTextValue(
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> classGroup,
            string attributeTextValue)
            => classGroup.SelectMany(x => x)
                    .Where(y => y.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText.Equals(attributeTextValue))
                    .GroupBy(z => z.Identifier.Text);
    }
}
