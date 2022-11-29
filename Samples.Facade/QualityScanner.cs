using DesignPatternCodeGenerator.Attributes.Facade;

namespace Samples.Facade;

public class QualityScanner
{
    [FacadeMethod("Scan")]
    public IEnumerable<string> QualityScan([FacadeMainParameter("githubUrl")] string githubUrl)
    {
        Console.WriteLine("Quality scan");

        return new List<string>() { "Error1", "Error2" };
    }
}
