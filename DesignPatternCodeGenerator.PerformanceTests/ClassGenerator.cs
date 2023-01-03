namespace DesignPatternCodeGenerator.PerformanceTests
{
    public class ClassGenerator
    {
        public static void GenerateClasses(int classCount)
        {
            var classContent = $@"using DesignPatternCodeGenerator.Attributes.IoCContainer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DesignPatternCodeGenerator.PerformanceTests;

interface IViewModel1
{{
}}

interface IViewModel2
{{
}}

interface IViewModel3 : IViewModel2
{{
}}

interface IViewModel4
{{
}}
";

            var services = "";

            for (int i = 1; i <= classCount; i++)
            {
                services += $@"
            services.AddSingleton<IViewModel1, MainViewModel{i}>();
            services.AddSingleton<IViewModel3, MainViewModel{i}>();
            services.AddSingleton<IViewModel2, MainViewModel{i}>();
";

                var classDeclaration = $@"

public class MainViewModel{i} : IViewModel1, IViewModel3, IViewModel4, IDisposable
{{
    public void Dispose()
    {{
    }}
}}

";
                classContent += classDeclaration;
            }

            var container = $@"
public static class AddViewModelsHostBuildersExtension
{{
        
    public static IHostBuilder AddViewModels(this IHostBuilder host)
    {{
        host.ConfigureServices(services =>
        {{
            {services}
        }});
            
        return host;
    }}
}}";

            classContent += container;

            var filepath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent + @"\Classes.cs";

            File.WriteAllText(filepath, classContent);
        }
    }

}
