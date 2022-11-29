using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DesignPatternCodeGenerator.Facade
{
    internal static class FacadeContentGenerator
    {
        internal static string GenerateClass(IGrouping<string, MethodDeclarationSyntax> group)
        => BaseCodeGenerator.GenerateUsingsAndNamespace(group) +
$@"
{{
    {GenerateClassDeclaration(group)}
    {{
        {GenerateFileds(group)}

        {GenerateMethod(group)}
    }}
}}";

        private static string GenerateClassDeclaration(IGrouping<string, MethodDeclarationSyntax> group)
            => $"public class {group.Key}Facade";

        private static string GenerateFileds(IGrouping<string, MethodDeclarationSyntax> group)
            => $"{string.Join("\n\t\t", GetClassDeclaration(group).Select(GenerateField))}";

        private static string GenerateField(ClassDeclarationSyntax classDeclaration)
            => $"private {GetClassName(classDeclaration)} _{GetFacadeFieldName(classDeclaration)} = new {GetClassName(classDeclaration)}();";

        private static string GenerateMethod(IGrouping<string, MethodDeclarationSyntax> group)
            => $@"public void {group.Key}({GenerateFacadeMethodParams(group)})
        {{
            {GenerateCallMethods(group)}
            
        }}";

        private static string GenerateCallMethods(IGrouping<string, MethodDeclarationSyntax> group)
        {
            var usedMethods = new List<string>();

            return GenerateNotDependencyMethod(group, ref usedMethods) + 
                "\n\n\t\t\t" + 
                GenerateDependencyMethod(group, ref usedMethods);
        }

        private static string GenerateDependencyMethod(
            IGrouping<string, MethodDeclarationSyntax> group,
            ref List<string> usedMethods)
            => GenerateParameterMethods(group, IsNotVoidMethod, ref usedMethods) + 
            GenerateParameterMethods(group, IsVoidMethod, ref usedMethods);

        private static string GenerateParameterMethods(
            IGrouping<string, MethodDeclarationSyntax> group,
            Func<MethodDeclarationSyntax, bool> voidMethod,
            ref List<string> usedMethods)
        {
            string generatedMethods = "";
            var unusedMethods = new List<MethodDeclarationSyntax>();

            var parameterMethods = GetMethods(group, voidMethod, "FacadeParameter");

            GenerateMethodsWithExistingParameters(parameterMethods, voidMethod, ref usedMethods, ref generatedMethods);

            return generatedMethods;
        }

        private static void GenerateMethodsWithExistingParameters(
            IEnumerable<MethodDeclarationSyntax> parameterMethods,                        
            Func<MethodDeclarationSyntax, bool> voidMethod,
            ref List<string> usedMethods,
            ref string generatedString)
        {
            var unusedMethods = new List<MethodDeclarationSyntax>();
            var counter = 0;

            foreach (var method in parameterMethods)
            {
                var parameters = GetParameterGroup(method, "FacadeParameter").Select(y => y.Key);

                if (IsFacadeContainsParams(usedMethods, parameters))
                {
                    generatedString += voidMethod == IsVoidMethod ?
                        GenerateVoidMethod(method, "FacadeParameter") :
                        GenerateNotVoidMethod(method, "FacadeParameter");

                    usedMethods.Add(GetNameToRegisterMethod(method));
                    counter++;
                }
                else
                {
                    unusedMethods.Add(method);
                }
            }

            if (unusedMethods.Count != 0 && counter > 0)
                GenerateMethodsWithExistingParameters(unusedMethods, voidMethod, ref usedMethods, ref generatedString);           
            
        }


        private static bool IsFacadeContainsParams(
            IEnumerable<string> facadeVariables,
            IEnumerable<string> parameters)
        {
            bool ret = true;

            parameters.ToList().ForEach(x =>
            {
                if (!facadeVariables.Contains(x))
                    ret = false;
            });

            return ret;
        }

        private static string GetNameToRegisterMethod(MethodDeclarationSyntax method)
            => GetClassName(GetClassDeclaration(method)) + "." + GetMethodName(method);

        private static string GenerateNotDependencyMethod(
            IGrouping<string, MethodDeclarationSyntax> group,
            ref List<string> usedMethods)
            => GenerateMainParameterMethods(group, IsNotVoidMethod, ref usedMethods) +
            GenerateMainParameterMethods(group, IsVoidMethod, ref usedMethods);

        private static string GenerateMainParameterMethods(
            IGrouping<string, MethodDeclarationSyntax> group,
            Func<MethodDeclarationSyntax, bool> voidMethod,
            ref List<string> usedMethods)
        {
            var mainParameterMethods = GetMethods(group, voidMethod, "FacadeMainParameter");

            usedMethods.AddRange(mainParameterMethods.ToList().Select(GetNameToRegisterMethod));

            return voidMethod == IsVoidMethod ?
                $"{string.Join("\n\t\t\t", mainParameterMethods.Select(x => GenerateVoidMethod(x, "FacadeMainParameter")))}" :
                $"{string.Join("\n\t\t\t", mainParameterMethods.Select(x => GenerateNotVoidMethod(x, "FacadeMainParameter")))}";
        }

        private static string GenerateNotVoidMethod(
            MethodDeclarationSyntax method,
            string attributeName)
            => $"var {GetVariableName(method)} = " +
            $"_{GetFacadeFieldName(GetClassDeclaration(method))}." +
            $"{GetMethodName(method)}({string.Join(", ", GetParameterGroup(method, attributeName).Select(GetParameterName))});";

        private static string GenerateVoidMethod(
            MethodDeclarationSyntax method,
            string attributeName)
            => $"_{GetFacadeFieldName(GetClassDeclaration(method))}." +
            $"{GetMethodName(method)}({string.Join(", ", GetParameterGroup(method, attributeName).Select(GetParameterName))});";

        private static string GetVariableName(MethodDeclarationSyntax method)
            => GetFacadeFieldName(GetClassDeclaration(method)) + GetMethodName(method);

        private static IEnumerable<MethodDeclarationSyntax> GetMethods(
            IGrouping<string, MethodDeclarationSyntax> group,
            Func<MethodDeclarationSyntax, bool> voidMethod,
            string attributeParameter)
            => group.Where(voidMethod)
            .Where(x => x.ParameterList.Parameters.First().AttributeLists.ToString().Contains(attributeParameter));

        private static string GenerateFacadeMethodParams(IGrouping<string, MethodDeclarationSyntax> group)
            => $"{string.Join(", ", GetParameterGroup(group, "FacadeMainParameter").Select(GenerateFacadeParameter))}";

        private static string GenerateFacadeParameter(IGrouping<string, ParameterSyntax> group)
            => $"{group.First().Type} {group.Key}";

        private static IEnumerable<ClassDeclarationSyntax> GetClassDeclaration(IGrouping<string, MethodDeclarationSyntax> group)
            => group.Select(x => x.Parent).OfType<ClassDeclarationSyntax>();
        private static ClassDeclarationSyntax GetClassDeclaration(MethodDeclarationSyntax method)
            => (ClassDeclarationSyntax)method.Parent;

        private static string GetClassName(ClassDeclarationSyntax classDeclaration)
            => classDeclaration.Identifier.Text;

        private static string GetFacadeFieldName(ClassDeclarationSyntax classDeclaration)
            => GetClassName(classDeclaration).Substring(0, 1).ToLower() + GetClassName(classDeclaration).Remove(0, 1);

        private static string GetMethodName(MethodDeclarationSyntax method)
            => method.Identifier.Text;

        private static string GetParameterName(IGrouping<string, ParameterSyntax> group)
            => group.Key.Substring(0, 1).ToLower() + group.Key.Replace(".", "").Remove(0, 1);

        private static bool IsVoidMethod(MethodDeclarationSyntax methodDeclarationSyntax)
            => methodDeclarationSyntax.ReturnType.ToString().Contains("void");

        private static bool IsNotVoidMethod(MethodDeclarationSyntax methodDeclarationSyntax) => !IsVoidMethod(methodDeclarationSyntax);

        private static IEnumerable<IGrouping<string, ParameterSyntax>> GetParameterGroup(
            IGrouping<string, MethodDeclarationSyntax> group,
            string attributeName)
            => group.Select(a => a.ParameterList)
                    .Select(b => b.Parameters)
                    .Where(c => c.First().AttributeLists.ToString().Contains(attributeName))
                    .SelectMany(d => d)
                    .GroupBy(e => e.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText);

        private static IEnumerable<IGrouping<string, ParameterSyntax>> GetParameterGroup(
            MethodDeclarationSyntax method,
            string attributeName)
            => method.ParameterList.Parameters.ToList()
                    .Where(c => c.AttributeLists.ToString().Contains(attributeName))
                    .GroupBy(e => e.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText);


    }
}
