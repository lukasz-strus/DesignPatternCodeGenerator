using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.NullObject
{
    public static class NullObjectContentComponentGenerator
    {
        internal static string GenerateMethods(IGrouping<string, InterfaceDeclarationSyntax> group)
        {
            var voidMethods = GetMethods(group, IsVoidMethod);
            var notVoidMethods = GetMethods(group, IsNotVoidMethod);

            return string.Join("\n\t\t", voidMethods.Select(GenerateVoidMethod)) + "\n\t\t" +
                string.Join("\n\t\t", notVoidMethods.Select(GenerateNotVoidMethod));
        }

        private static string GenerateVoidMethod(MethodDeclarationSyntax method)
            => $"public void {method.Identifier.Text}({string.Join(", ", GetParameters(method).Select(GetParameter))}) {{ }}";

        private static string GenerateNotVoidMethod(MethodDeclarationSyntax method)
            => $@"public {method.ReturnType} {method.Identifier.Text}({string.Join(", ", GetParameters(method).Select(GetParameter))}) 
        {{
            return default({method.ReturnType});
        }}";

        private static string GetParameter(ParameterSyntax parameter)
            => $"{parameter.Type} {parameter.Identifier.Text}";

        private static IEnumerable<ParameterSyntax> GetParameters(MethodDeclarationSyntax method)
            => method.ParameterList.Parameters.ToList();

        internal static string GenerateProperties(IGrouping<string, InterfaceDeclarationSyntax> group)
        {
            var properties = GetProperties(group);
            return $"{string.Join("\n\t\t", properties.Select(GenerateProperty))}";
        }

        private static string GenerateProperty(PropertyDeclarationSyntax property)
            => $"public {property.Type} {property.Identifier.Text} {{ {property.AccessorList.Accessors} }}";

        private static IEnumerable<PropertyDeclarationSyntax> GetProperties(IGrouping<string, InterfaceDeclarationSyntax> group)
            => group.SelectMany(g => g.Members).OfType<PropertyDeclarationSyntax>().Distinct();

        private static IEnumerable<MethodDeclarationSyntax> GetMethods(
            IGrouping<string, InterfaceDeclarationSyntax> group,
            Func<MethodDeclarationSyntax, bool> isVoidMethod)
            => group.SelectMany(g => g.Members)
            .OfType<MethodDeclarationSyntax>()
            .Where(isVoidMethod)
            .Distinct();

        private static bool IsVoidMethod(MethodDeclarationSyntax methodDeclarationSyntax)
            => methodDeclarationSyntax.ReturnType.ToString().Contains("void");

        private static bool IsNotVoidMethod(MethodDeclarationSyntax methodDeclarationSyntax)
            => !IsVoidMethod(methodDeclarationSyntax);
    }
}
