using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.ContainerIOC.Components
{
    internal static class ContainerClassComponentsGenerator
    {
        internal static string GenerateDeclaration(IGrouping<string, ClassDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} static class {GetContainerName(group)}";

        internal static string GenerateMethod(IGrouping<string, ClassDeclarationSyntax> group, GeneratorExecutionContext context)
            => $@"
        {GetMethodName(group)}
        {{
            host.ConfigureServices(services =>
            {{
                {GenerateRegister(group, context)}
            }});
            
            return host;
        }}";

        private static string GenerateRegister(IGrouping<string, ClassDeclarationSyntax> group, GeneratorExecutionContext context)
            => $"{string.Join("\n\t\t\t\t", group.Select(x=>GenerateAddRegisters(x, context)))}";

        private static string GenerateAddRegisters(ClassDeclarationSyntax syntax, GeneratorExecutionContext context)
             => $"{string.Join("\n\t\t\t\t", GetAllClassInterfaces(syntax, context).Select(x => GenerateAddRegister(syntax, x)))}";

        private static string GenerateAddRegister(ClassDeclarationSyntax syntax, string interfaceName)
            => $"services.Add{GetObjectLife(syntax)}<{interfaceName}, {syntax.Identifier.Text}>();";

        private static string GetMethodName(IGrouping<string, ClassDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} static IHostBuilder {BaseNamesGenerator.GetClassName(group)}(this IHostBuilder host)";

        private static string GetContainerName(IGrouping<string, ClassDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetClassName(group)}HostBuildersExtension";

        private static IEnumerable<string> GetAllClassInterfaces(
            ClassDeclarationSyntax syntax,
            GeneratorExecutionContext context)
        {
            var classDeclaration = syntax;

            var semanticModel = context.Compilation.GetSemanticModel(classDeclaration.SyntaxTree);

            var interfaces = semanticModel.GetDeclaredSymbol(classDeclaration).AllInterfaces;

            var allInterfaces = interfaces.Select(x => x.ToString())
                                          .Where(RemoveSystemInterfaces)
                                          .Select(GetInterfaceName);

            return allInterfaces.Except(GetExcludedInterfaces(syntax));
        }

        private static IEnumerable<string> GetExcludedInterfaces(ClassDeclarationSyntax syntax)
        {
            var argument = GetAttributeArgument(syntax, typeof(ArrayCreationExpressionSyntax));

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
                    .First();

        private static bool RemoveSystemInterfaces(string fullName)
            => !fullName.Contains("System.");

        private static string GetInterfaceName(string fullName)
            => fullName.Substring(fullName.LastIndexOf('.') + 1);
    }
}
