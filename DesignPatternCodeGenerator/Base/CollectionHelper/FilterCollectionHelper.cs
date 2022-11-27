using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.CollectionHelper
{
    internal static class FilterCollectionHelper
    {
        internal static IEnumerable<ClassDeclarationSyntax> FilterClassDeclarationsByTypes(
            IEnumerable<ClassDeclarationSyntax> classDeclarations,
            string typeName)
            => classDeclarations.Where(y => IsClassType(y, typeName));

        private static bool IsClassType(ClassDeclarationSyntax classSyntax, string typeName)
        {
            var nullableType = typeName.EndsWith("?") ? typeName.Remove(typeName.Length - 1) : typeName;

            return classSyntax.Identifier.Text.Contains(nullableType);
        }

        internal static IEnumerable<PropertyDeclarationSyntax> FilterPropertyByTypes(
            IEnumerable<PropertyDeclarationSyntax> classGroup,
            string typeName)
            => classGroup.Where(y => IsPropertyType(y, typeName));

        private static bool IsPropertyType(PropertyDeclarationSyntax property, string typeName)
        {
            var propertyType = property.Type.ToString();

            var nullableType = propertyType.EndsWith("?") ? propertyType.Remove(propertyType.Length - 1) : propertyType;

            return nullableType.Contains(typeName);
        }

        internal static IEnumerable<PropertyDeclarationSyntax> FilterPropertyByTypes(
            IEnumerable<PropertyDeclarationSyntax> classGroup,
            IEnumerable<string> type)
        {
            var ret = new List<PropertyDeclarationSyntax>();

            type.ToList().ForEach(
                x =>
                {
                    ret.AddRange(FilterPropertyByTypes(classGroup, x));
                });

            return ret;
        }


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
