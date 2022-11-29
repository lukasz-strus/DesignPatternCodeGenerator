using DesignPatternCodeGenerator.Attributes.Facade;

namespace Samples.Facade;

public class ReportGenerator
{
    [FacadeMethod("Scan")]
    public void GenerateReport(
        [FacadeParameter("QualityScanner.QualityScan")] IEnumerable<string> qualityScanErrors,
        [FacadeParameter("SecurityScanner.SecurityScan")] IEnumerable<string> securityScanErrors,
        [FacadeParameter("DependencyScanner.DependencyScan")] IEnumerable<string> dependencyScanErrors)
    {
        Console.WriteLine("Quality Scan Errors:");
        Console.WriteLine(string.Join(", ", qualityScanErrors));

        Console.WriteLine("Security Scan Errors:");
        Console.WriteLine(string.Join(", ", securityScanErrors));

        Console.WriteLine("Dependency Scan Errors:");
        Console.WriteLine(string.Join(", ", dependencyScanErrors));
    }
}
