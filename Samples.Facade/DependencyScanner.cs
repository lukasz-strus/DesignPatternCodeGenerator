using DesignPatternCodeGenerator.Attributes.Facade;

namespace Samples.Facade;

public class DependencyScanner
{
    [FacadeMethod("Scan")]
    public IEnumerable<string> DependencyScan([FacadeMainParameter("githubUrl")]string githubUrl)
    {
        Console.WriteLine("Dependency Scan");

        return new List<string>() { "Dependency Error1" };
    }

    [FacadeMethod("Scan")]
    public void DependencyScanRaport([FacadeParameter("DependencyScanner.DependencyScan")] IEnumerable<string> dependencyScanErrors)
    {
        Console.WriteLine("Dependency Scan");
    }
}
