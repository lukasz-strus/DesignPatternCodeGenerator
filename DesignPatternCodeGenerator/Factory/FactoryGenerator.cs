using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Base.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace DesignPatternCodeGenerator.Factory
{
    [Generator]
    public class FactoryGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var factoryAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.Factory);
            var factoryChildAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.FactoryChild);

            var interfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(
                context.Compilation,
                context.CancellationToken,
                factoryAttribute);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                factoryChildAttribute);

            var configuration = new SyntaxTokensConfigurations()
            {
                IsDesignPatternPostfix = true,
                IsMainAttributeOnInterface = true
            };


            foreach (var group in interfaceGroups)
            {
                var syntaxTokens = SyntaxTokensGenerator.GenerateSyntaxTokens(group, GeneratorAttributeType.Factory, configuration);

                var codeGenerator = new BaseCodeGenerator(syntaxTokens, configuration);

                var factoryChildGroups = FactoryChild.FilterFactoryChild(classGroups, syntaxTokens.InterfaceName.Replace("Factory", ""));

                var enumChildContent = FactoryChildGenerator.GenerateEnum(codeGenerator, factoryChildGroups);
                context.AddSource($"{syntaxTokens.ClassName}Type.g.cs", SourceText.From(enumChildContent, Encoding.UTF8));

                var interfaceContent = FactoryContentGenerator.GenerateInterface(codeGenerator, group);
                context.AddSource($"{syntaxTokens.InterfaceName}.g.cs", SourceText.From(interfaceContent, Encoding.UTF8));

                var classContent = FactoryContentGenerator.GenerateClass(codeGenerator, group, factoryChildGroups);
                context.AddSource($"{syntaxTokens.ClassName}.g.cs", SourceText.From(classContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
