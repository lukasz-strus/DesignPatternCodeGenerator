using DesignPatternCodeGenerator.Attributes.Facade;

namespace Samples.Facade;

public class SecurityScanner
{
    public IEnumerable<string> SecurityScan([FacadeMainParameter("githubUrl")] string githubUrl)
    {
        Console.WriteLine("Security scan");

        return new List<string>() { "security error1" };
    }
}
