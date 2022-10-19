using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Factory;
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
        private readonly Type _generatorType;

        internal IEnumerable<InterfaceDeclarationSyntax> Interfaces { get; }
        internal IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> InterfaceGroups { get; }

        internal DeclarationsSyntaxGenerator(GeneratorExecutionContext context, GeneratorType generatorType)
        {
            _generatorType = SetGeneratorType(generatorType);

            Interfaces = SetInterfaceDeclarations(context).Result;
            InterfaceGroups = Interfaces.GroupBy(x => x.Identifier.Text);
        }

        private async Task<IEnumerable<InterfaceDeclarationSyntax>> SetInterfaceDeclarations(GeneratorExecutionContext context)
        {
            var compilation = context.Compilation;
            var token = context.CancellationToken;

            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => SetInterfaceDeclarationSyntax(x, compilation, token)))).SelectMany(x => x);
        }

        private async Task<IEnumerable<InterfaceDeclarationSyntax>> SetInterfaceDeclarationSyntax(
            SyntaxTree tree,
            Compilation compilation,
            CancellationToken token)
        {
            var semanticModel = compilation.GetSemanticModel(tree);

            var interfaces = (await tree.GetRootAsync(token))
                .DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                .Where(x => x.AttributeLists.Any());

            return interfaces.Where(x => x.AttributeLists.Any(y => y.Attributes.Any(z => semanticModel.GetTypeInfo(z).Type.Name == _generatorType.Name)));
        }

        private Type SetGeneratorType(GeneratorType generatorType)
        {
            switch (generatorType)
            {
                case GeneratorType.Factory:
                    return typeof(FactoryAttribute);
                default:
                    throw new Exception($"Type {generatorType} is not handled");
            }
        }
    }
}
