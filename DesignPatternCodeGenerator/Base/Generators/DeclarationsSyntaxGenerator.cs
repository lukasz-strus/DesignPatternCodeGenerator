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
            var classes = SetClassDeclarations(compilation, token).Result;

            return classes.GroupBy(x => x.Identifier.Text);
        }

        internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GetClassGroups(
            Compilation compilation,
            CancellationToken token,
            Type attributeType)
        {
            var classes = SetClassDeclarations(compilation, token, attributeType).Result;

            return classes.GroupBy(x => x.Identifier.Text);
        }

        internal static IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> GetInterfaceGroups(
            Compilation compilation,
            CancellationToken token,
            Type attributeType)
        {
            var interfaces = SetInterfaceDeclarations(compilation, token, attributeType).Result;

            return interfaces.GroupBy(x => x.Identifier.Text);
        }

        private static async Task<IEnumerable<ClassDeclarationSyntax>> SetClassDeclarations(
            Compilation compilation, 
            CancellationToken token,
            Type attributeType)
        {
            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => SetClassDeclarationSyntax(x, compilation, token, attributeType))))
                .SelectMany(x => x);
        }
        private static async Task<IEnumerable<ClassDeclarationSyntax>> SetClassDeclarations(
            Compilation compilation,
            CancellationToken token)
        {
            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => SetClassDeclarationSyntax(x, token))))
                .SelectMany(x => x);
        }

        private static async Task<IEnumerable<ClassDeclarationSyntax>> SetClassDeclarationSyntax(
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

            return classes.Where(x => x.AttributeLists.Any(y => y.Attributes.Any(z => semanticModel.GetTypeInfo(z).Type.Name == attributeType.Name)));
        }

        private static async Task<IEnumerable<ClassDeclarationSyntax>> SetClassDeclarationSyntax(
            SyntaxTree tree,
            CancellationToken token)
        {
            return (await tree.GetRootAsync(token))
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>();
        }

        private static async Task<IEnumerable<InterfaceDeclarationSyntax>> SetInterfaceDeclarations(
            Compilation compilation,
            CancellationToken token,
            Type attributeType)
        {
            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => SetInterfaceDeclarationSyntax(x, compilation, token, attributeType))))
                .SelectMany(x => x);
        }

        private static async Task<IEnumerable<InterfaceDeclarationSyntax>> SetInterfaceDeclarationSyntax(
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
    }
}
