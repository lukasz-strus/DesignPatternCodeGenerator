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
            var properties = GetProperties(group);
            var typesName = GetTypesName(allClassGroups);
            var filtredProperties = FilterCollectionHelper.FilterPropertyByTypes(properties, typesName);

            return $"{string.Join("\n", filtredProperties.Select(p => $"clone.{p.Identifier.Text} = {GenerateNewObject(GetPropertyClassDeclaration(allClassGroups, p), p.Identifier.Text)}"))}";
        }

        private static IEnumerable<string> GetTypesName(
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> allClassGroups)
            => allClassGroups.SelectMany(x => x).Select(y => y.Identifier.Text);

        private static ClassDeclarationSyntax GetPropertyClassDeclaration(
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> allClassGroups,
            PropertyDeclarationSyntax property)
            => allClassGroups.SelectMany(x => x).Where(y => y.Identifier.Text == property.Type.ToString()).First();


        private static string GenerateNewObject(ClassDeclarationSyntax classDeclaration, string objectName)
            => $@"new {classDeclaration.Identifier.Text}()
            {{
                {GenerateAssignFields(classDeclaration, objectName)}
            }};";

        private static string GenerateAssignFields(ClassDeclarationSyntax classDeclaration, string objectName)
        {
            var properties = classDeclaration.Members.OfType<PropertyDeclarationSyntax>();

            return string.Join(",\n ", properties.Select(p => GenerateAssign(p, objectName)));
        }

        private static string GenerateAssign(PropertyDeclarationSyntax p, string objectName)
        => $"{p.Identifier.Text} = {objectName}.{p.Identifier.Text}";

        internal static string GenerateShallowClone(IGrouping<string, ClassDeclarationSyntax> group)
            => $@"{GenerateMethodDeclaration(group, CopyType.Shallow)}
        {{
            return {GenerateShallowMethod(group)}
        }}";

        private static string GenerateShallowMethod(IGrouping<string, ClassDeclarationSyntax> group)
            => $"({group.Key})this.MemberwiseClone();";

        private static string GenerateMethodDeclaration(IGrouping<string, ClassDeclarationSyntax> group, CopyType type)
            => $"public {group.Key} {type}Copy()";

        private static IEnumerable<PropertyDeclarationSyntax> GetProperties(
            IEnumerable<TypeDeclarationSyntax> group)
            => group.SelectMany(g => g.Members).OfType<PropertyDeclarationSyntax>().Distinct();
    }
}
