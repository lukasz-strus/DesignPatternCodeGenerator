using DesignPatternCodeGenerator.Base.CollectionHelper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Prototype
{
    internal static class PrototypeContentComponentsGenerator
    {
        internal static string GenerateDeepClone(
            IGrouping<string, ClassDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> allClassGroups)
            => $@"{GenerateMethodDeclaration(group, CopyType.Deep)}
        {{
            {group.Key} clone = {GenerateShallowMethod(group)}

            {GenerateObjectFieldsClone(group, allClassGroups)}

            return clone;
        }}";

        private static string GenerateObjectFieldsClone(
            IGrouping<string, ClassDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> allClassGroups)
        {
            var properties = group.SelectMany(g => g.Members).OfType<PropertyDeclarationSyntax>().Distinct();

            var typesName = allClassGroups.SelectMany(x => x).Select(y => y.Identifier.Text);

            var filtredProperties = FilterCollectionHelper.FilterPropertyByTypes(properties, typesName);

            return $"{string.Join("\n\n\t\t\t", filtredProperties.Select(p => $"clone.{p.Identifier.Text} = {GenerateNewObject(allClassGroups, p)}"))}";
        }

        private static string GenerateNewObject(
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> allClassGroups, 
            PropertyDeclarationSyntax property)
        {
            var classDeclaration = allClassGroups.SelectMany(x => x)
                .Where(y => y.Identifier.Text == property.Type.ToString())
                .First();

            return $@"new {classDeclaration.Identifier.Text}()
            {{
                {GenerateAssignFields(classDeclaration, property)}
            }};";
        }

        private static string GenerateAssignFields(ClassDeclarationSyntax classDeclaration, PropertyDeclarationSyntax propertyObject)
        {
            var properties = classDeclaration.Members.OfType<PropertyDeclarationSyntax>();

            return string.Join(",\n\t\t\t\t", properties.Select(p => GenerateAssign(p, propertyObject)));
        }

        private static string GenerateAssign(PropertyDeclarationSyntax propertyToAssign, PropertyDeclarationSyntax propertyObject)
            => $"{propertyToAssign.Identifier.Text} = {propertyObject.Identifier.Text}.{propertyToAssign.Identifier.Text}";

        internal static string GenerateShallowClone(IGrouping<string, ClassDeclarationSyntax> group)
            => $@"{GenerateMethodDeclaration(group, CopyType.Shallow)}
        {{
            return {GenerateShallowMethod(group)}
        }}";

        private static string GenerateShallowMethod(IGrouping<string, ClassDeclarationSyntax> group)
            => $"({group.Key})this.MemberwiseClone();";

        private static string GenerateMethodDeclaration(IGrouping<string, ClassDeclarationSyntax> group, CopyType type)
            => $"public {group.Key} {type}Copy()";
    }
}
