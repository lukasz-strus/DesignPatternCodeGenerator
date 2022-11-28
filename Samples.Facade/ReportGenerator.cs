using DesignPatternCodeGenerator.Attributes.Facade;

namespace Samples.Facade;

public class ReportGenerator
{
    [FacadeMethod("Scan")]
    public void GenerateReport(
        [FacadeParameter("Samples.Facade.QualityScanner.QualityScan")] IEnumerable<string> qualityScanErrors,
        [FacadeParameter("Samples.Facade.SecurityScanner.SecurityScan")] IEnumerable<string> securityScanErrors,
        [FacadeParameter("Samples.Facade.DependencyScanner.DependencyScan")] IEnumerable<string> dependencyScanErrors)
    {
        Console.WriteLine("Quality Scan Errors:");
        Console.WriteLine(string.Join(", ", qualityScanErrors));

        Console.WriteLine("Security Scan Errors:");
        Console.WriteLine(string.Join(", ", securityScanErrors));

        Console.WriteLine("Dependency Scan Errors:");
        Console.WriteLine(string.Join(", ", dependencyScanErrors));
    }
}
