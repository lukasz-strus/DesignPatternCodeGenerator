using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace DesignPatternCodeGenerator.IoCContainer
{
    [Generator]
    public class ContainerGenerator : ISourceGenerator
    {
        //TODO zmienić z IHostBuilder na IServiceCollection
        public void Execute(GeneratorExecutionContext context)
        {
            var containerAttribute = AttributeTypeGenerator.CreateGeneratorAttributeType(GeneratorAttributeType.Container);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                containerAttribute);

            classGroups = classGroups.GroupByAttribute();

            foreach (var group in classGroups)
            {
                GenerateContainer(context, group);
            }
        }

        private void GenerateContainer(
            GeneratorExecutionContext context,
            IGrouping<string, ClassDeclarationSyntax> group)
        {
            var hintName = $"{BaseNamesGenerator.GetClassName(group)}HostBuildersExtension.g.cs";
            var classContent = ContainerContentGenerator.GenerateClass(group, context.Compilation);

            context.AddSource(hintName, SourceText.From(classContent, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }

    }
}
