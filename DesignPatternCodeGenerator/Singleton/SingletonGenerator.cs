using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Base.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace DesignPatternCodeGenerator.Singleton
{
    [Generator]
    public class SingletonGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var singletonAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.Singleton);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                singletonAttribute);

            var configuration = new SyntaxTokensConfigurations()
            {
                IsPartialClass = true
            };

            foreach (var group in classGroups)
            {
                var syntaxTokens = SyntaxTokensGenerator.GenerateSyntaxTokens(group, GeneratorAttributeType.Singleton, configuration);

                var codeGenerator = new BaseCodeGenerator(syntaxTokens, configuration);

                var classContent = SingletonContentGenerator.GenerateClass(codeGenerator, group);
                context.AddSource($"{syntaxTokens.ClassName.Replace("Singleton", "")}.g.cs", SourceText.From(classContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
