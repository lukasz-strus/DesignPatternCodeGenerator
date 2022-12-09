using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.CollectionHelper
{
    internal static class FilterCollectionHelper
    {
        internal static IEnumerable<ClassDeclarationSyntax> FilterByTypes(
            this IEnumerable<ClassDeclarationSyntax> classDeclarations,
            string typeName)
            => classDeclarations.Where(y => IsClassType(y, typeName));

        private static bool IsClassType(ClassDeclarationSyntax classSyntax, string typeName)
        {
            var nullableType = typeName.EndsWith("?") ? typeName.Remove(typeName.Length - 1) : typeName;

            return classSyntax.Identifier.Text.Contains(nullableType);
        }

        internal static IEnumerable<PropertyDeclarationSyntax> FilterByTypes(
            this IEnumerable<PropertyDeclarationSyntax> classGroup,
            string typeName)
            => classGroup.Where(y => IsPropertyType(y, typeName));

        private static bool IsPropertyType(PropertyDeclarationSyntax property, string typeName)
        {
            var propertyType = property.Type.ToString();

            var nullableType = propertyType.EndsWith("?") ? propertyType.Remove(propertyType.Length - 1) : propertyType;

            return nullableType.Contains(typeName);
        }

        internal static IEnumerable<PropertyDeclarationSyntax> FilterByTypes(
            this IEnumerable<PropertyDeclarationSyntax> classGroup,
            IEnumerable<string> type)
        {
            var ret = new List<PropertyDeclarationSyntax>();

            type.ToList().ForEach(
                x =>
                {
                    ret.AddRange(classGroup.FilterByTypes(x));
                });

            return ret;
        }

        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> FilterByInterface(
            this IEnumerable<IGrouping<string, ClassDeclarationSyntax>> classGroup,
            string interfaceName)
            => classGroup.SelectMany(x => x)
                    .Where(y => y.FirstAncestorOrSelf<ClassDeclarationSyntax>().BaseList.Types.ToString().Equals(interfaceName))
                    .GroupBy(z => z.Identifier.Text);

        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> FilterByInterface(
            this IEnumerable<IGrouping<string, ClassDeclarationSyntax>> classGroup,
            IEnumerable<string> interfaceName)
        {
            var ret = new List<IGrouping<string, ClassDeclarationSyntax>>();

            interfaceName.ToList().ForEach(
                x =>
                {
                    ret.AddRange(classGroup.FilterByInterface(x));
                });

            return ret.Distinct();
        }
    }
}
