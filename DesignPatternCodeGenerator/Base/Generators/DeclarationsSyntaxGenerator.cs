using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Base.Generators
{
    internal class DeclarationsSyntaxGenerator
    {
        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GetAllClassGroups(
            Compilation compilation,
            CancellationToken token)
        {
            var classes = CreateClassDeclarations(compilation, token).Result;

            return classes.GroupBy(x => x.Identifier.Text);
        }

        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GetClassGroups(
            Compilation compilation,
            CancellationToken token,
            Type attributeType)
        {
            var classes = CreateClassDeclarations(compilation, token, attributeType).Result;

            return classes.GroupBy(x => x.Identifier.Text);
        }

        internal static IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> GetInterfaceGroups(
            Compilation compilation,
            CancellationToken token,
            Type attributeType)
        {
            var interfaces = CreateInterfaceDeclarations(compilation, token, attributeType).Result;

            return interfaces.GroupBy(x => x.Identifier.Text);
        }

        internal static IEnumerable<IGrouping<string, MethodDeclarationSyntax>> GetMethodGroups(
            Compilation compilation, 
            CancellationToken token, 
            Type attributeType)
        {
            var methods = CreateMethodDeclarations(compilation, token, attributeType).Result;

            return methods.GroupBy(x => x.Identifier.Text);
        }

        private static async Task<IEnumerable<MethodDeclarationSyntax>> CreateMethodDeclarations(
            Compilation compilation, 
            CancellationToken token, 
            Type attributeType)
        {
            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => CreateMethodDeclarationSyntax(x, token, attributeType))))
                .SelectMany(x => x);
        }

        private static async Task<IEnumerable<MethodDeclarationSyntax>> CreateMethodDeclarationSyntax(
            SyntaxTree tree, 
            CancellationToken token, 
            Type attributeType)
        {
            var methods = (await tree.GetRootAsync(token))
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(x => x.AttributeLists.Any());

            return methods.Where(x => x.AttributeLists.ToString().Contains(attributeType.Name.Replace("Attribute", "")));
        }

        private static async Task<IEnumerable<ClassDeclarationSyntax>> CreateClassDeclarations(
            Compilation compilation, 
            CancellationToken token,
            Type attributeType)
        {
            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => CreateClassDeclarationSyntax(x, compilation, token, attributeType))))
                .SelectMany(x => x);
        }
        private static async Task<IEnumerable<ClassDeclarationSyntax>> CreateClassDeclarations(
            Compilation compilation,
            CancellationToken token)
        {
            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => CreateClassDeclarationSyntax(x, token))))
                .SelectMany(x => x);
        }

        private static async Task<IEnumerable<ClassDeclarationSyntax>> CreateClassDeclarationSyntax(
            SyntaxTree tree,
            Compilation compilation,
            CancellationToken token,
            Type attributeType)
        {
            var semanticModel = compilation.GetSemanticModel(tree);

            var classes = (await tree.GetRootAsync(token))
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Where(x => x.AttributeLists.Any());

            if (IsContainerAttribute(attributeType))
            {
                return classes.Where(x => x.AttributeLists.Any(y => y.Attributes.Any(z => z.Name.ToString().Contains("Container"))));
            }

            return classes.Where(x => x.AttributeLists.Any(y => y.Attributes.Any(z => semanticModel.GetTypeInfo(z).Type.Name == attributeType.Name)));
        }

        private static async Task<IEnumerable<ClassDeclarationSyntax>> CreateClassDeclarationSyntax(
            SyntaxTree tree,
            CancellationToken token)
        {
            return (await tree.GetRootAsync(token))
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>();
        }

        private static async Task<IEnumerable<InterfaceDeclarationSyntax>> CreateInterfaceDeclarations(
            Compilation compilation,
            CancellationToken token,
            Type attributeType)
        {
            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => CreateInterfaceDeclarationSyntax(x, compilation, token, attributeType))))
                .SelectMany(x => x);
        }

        private static async Task<IEnumerable<InterfaceDeclarationSyntax>> CreateInterfaceDeclarationSyntax(
            SyntaxTree tree,
            Compilation compilation,
            CancellationToken token,
            Type attributeType)
        {
            var semanticModel = compilation.GetSemanticModel(tree);

            var interfaces = (await tree.GetRootAsync(token))
                .DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                .Where(x => x.AttributeLists.Any());

            return interfaces.Where(x => x.AttributeLists.Any(y => y.Attributes.Any(z => semanticModel.GetTypeInfo(z).Type.Name == attributeType.Name)));
        }

        private static bool IsContainerAttribute(Type attributeType) 
            => attributeType == AttributeTypeGenerator.CreateGeneratorAttributeType(GeneratorAttributeType.Container);
    }
}
