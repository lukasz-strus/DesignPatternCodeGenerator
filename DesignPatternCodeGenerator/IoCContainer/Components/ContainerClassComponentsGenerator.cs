using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.IoCContainer.Components
{
    internal static class ContainerClassComponentsGenerator
    {
        internal static string GenerateDeclaration(IGrouping<string, ClassDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} static class {GetContainerName(group)}";

        internal static string GenerateMethod(IGrouping<string, ClassDeclarationSyntax> group, Compilation compilation)
            => $@"
        {GetMethodName(group)}
        {{
            {GenerateRegister(group, compilation)}
            
            return services;
        }}";

        private static string GenerateRegister(IGrouping<string, ClassDeclarationSyntax> group, Compilation compilation)
            => $"{string.Join("\n\t\t\t\t", group.Select(x=>GenerateAddRegisters(x, compilation)))}";

        private static string GenerateAddRegisters(ClassDeclarationSyntax syntax, Compilation compilation)
             => $"{string.Join("\n\t\t\t\t", GetClassInterfaces(syntax, compilation).Select(x => GenerateAddRegister(syntax, x)))}";

        private static string GenerateAddRegister(ClassDeclarationSyntax syntax, string interfaceName)
            => $"services.Add{GetObjectLife(syntax)}<{interfaceName}, {syntax.Identifier.Text}>();";

        private static string GetMethodName(IGrouping<string, ClassDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} static IServiceCollection {BaseNamesGenerator.GetClassName(group)}(this IServiceCollection services)";

        private static string GetContainerName(IGrouping<string, ClassDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetClassName(group)}ServiceCollectionExtension";

        private static IEnumerable<string> GetClassInterfaces(
            ClassDeclarationSyntax syntax,
            Compilation compilation)
        {
            var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);

            var allInterfaces = semanticModel.GetDeclaredSymbol(syntax).AllInterfaces;

            var interfaces = allInterfaces.Select(x => x.ToString())
                                          .Where(RemoveSystemInterfaces)
                                          .Select(GetInterfaceName);

            var excludedInterface = GetExcludedInterfaces(syntax);

            if (excludedInterface == null)
                return interfaces;

            return interfaces.Except(excludedInterface);
        }

        private static IEnumerable<string> GetExcludedInterfaces(ClassDeclarationSyntax syntax)
        {
            var argument = GetAttributeArgument(syntax, typeof(ArrayCreationExpressionSyntax));

            if (argument == null)
                return null;

            var arrayExpression = (ArrayCreationExpressionSyntax)argument.Expression;

            return arrayExpression.Initializer.Expressions
                                  .ToList()
                                  .Select(x => (LiteralExpressionSyntax)x)
                                  .Select(y => y.Token.ValueText);
        }

        private static string GetObjectLife(ClassDeclarationSyntax group)
        {
            var argument = GetAttributeArgument(group, typeof(MemberAccessExpressionSyntax));

            var expresion = (MemberAccessExpressionSyntax)argument.Expression;

            return expresion.Name.Identifier.ValueText;
        }

        private static AttributeArgumentSyntax GetAttributeArgument(ClassDeclarationSyntax group, Type type)
            => group.AttributeLists
                    .First().Attributes
                    .First().ArgumentList.Arguments
                    .Where(x => x.Expression.GetType() == type)
                    .FirstOrDefault();

        private static bool RemoveSystemInterfaces(string fullName)
            => !fullName.Contains("System.");

        private static string GetInterfaceName(string fullName)
            => fullName.Substring(fullName.LastIndexOf('.') + 1);
    }
}
