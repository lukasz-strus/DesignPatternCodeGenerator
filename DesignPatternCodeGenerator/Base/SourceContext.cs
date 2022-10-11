using DesignPatternCodeGenerator.Factory;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Base
{
    internal class SourceContext
    {
        private readonly Type _generatorType;

        internal IEnumerable<ConstructorDeclarationSyntax> Constructors { get; }

        internal SourceContext(GeneratorExecutionContext context, GeneratorType generatorType)
        {
            _generatorType = SetGeneratorType(generatorType);

            Constructors = SetConstructorDeclarations(context).Result;
        }

        private async Task<IEnumerable<ConstructorDeclarationSyntax>> SetConstructorDeclarations(GeneratorExecutionContext context)
        {
            var compilation = context.Compilation;
            var token = context.CancellationToken;

            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => SetConstructorDeclarationSyntax(x, compilation, token)))).SelectMany(x => x);
        }

        private async Task<IEnumerable<ConstructorDeclarationSyntax>> SetConstructorDeclarationSyntax(
            SyntaxTree tree,
            Compilation compilation,
            CancellationToken token)
        {
            var semanticModel = compilation.GetSemanticModel(tree);

            var constructors = (await tree.GetRootAsync(token))
                .DescendantNodes()
                .OfType<ConstructorDeclarationSyntax>()
                .Where(x => x.AttributeLists.Any());

            return constructors.Where(x => x.AttributeLists.Any(y => y.Attributes.Any(z => semanticModel.GetTypeInfo(z).Type.Name == _generatorType.Name)));
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
