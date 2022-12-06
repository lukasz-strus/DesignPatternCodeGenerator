﻿using DesignPatternCodeGenerator.Base.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.Generators
{
    internal static class BaseCodeGenerator
    {
        internal static string GenerateUsingsAndNamespace(IGrouping<string, TypeDeclarationSyntax> mainGroup)
            => $@"// <auto-generated/>
{GenerateUsings(mainGroup)}

namespace {BaseNamesGenerator.GetNamespace(mainGroup)}";

        internal static string GenerateUsingsAndNamespace(
            IGrouping<string, TypeDeclarationSyntax> mainGroup,
            IGrouping<string, TypeDeclarationSyntax> additionalGroupToUsings)
            => $@"// <auto-generated/>
{GenerateUsings(mainGroup)}
{GenerateAdditionalUsing(additionalGroupToUsings)}

namespace {BaseNamesGenerator.GetNamespace(mainGroup)}";

        internal static string GenerateUsingsAndNamespace(IGrouping<string, MethodDeclarationSyntax> group)
            => $@"// <auto-generated/>
{GenerateUsings(group)}

namespace {BaseNamesGenerator.GetNamespace(group)}";

        internal static string GenerateDeclaration(
            IGrouping<string, TypeDeclarationSyntax> group,
            CodeType codeType,
            bool isMainAttributeOnInterface = false,
            bool isPartialClass = false,
            bool isDesignPatternPostfix = false,
            GeneratorAttributeType attributeType = GeneratorAttributeType.Pattern)
        {
            switch (codeType)
            {
                case CodeType.Interface:
                    return GenerateInterfaceDeclaration(group, isDesignPatternPostfix, attributeType);
                case CodeType.Class:
                    return GenerateClassDeclaration(group, isDesignPatternPostfix, isMainAttributeOnInterface, isPartialClass, attributeType);
                case CodeType.Enum:
                    return GenerateEnumDeclaration(group, isDesignPatternPostfix, isMainAttributeOnInterface, attributeType);
                default:
                    return "";
            }
        }

        private static string GenerateUsings(IGrouping<string, TypeDeclarationSyntax> group)
            => $@"{string.Join("\n", BaseNamesGenerator.GetUsings(group).Select(x => $"using {x};"))}";
        private static string GenerateUsings(IGrouping<string, MethodDeclarationSyntax> group)
             => $@"{string.Join("\n", BaseNamesGenerator.GetUsings(group).Select(x => $"using {x};"))}";

        private static string GenerateAdditionalUsing(IGrouping<string, TypeDeclarationSyntax> group)
        {
            return $@"{string.Join("\n", $"using {BaseNamesGenerator.GetNamespace(group)};")}";
        }

        private static string GenerateInterfaceDeclaration(
            IGrouping<string, TypeDeclarationSyntax> group,
            bool isDesignPatternPostfix,
            GeneratorAttributeType attributeType = GeneratorAttributeType.Pattern)
            => $"{BaseNamesGenerator.GetAccesibility(group)} interface " +
            (isDesignPatternPostfix ? $"{BaseNamesGenerator.GetInterfaceName(group, attributeType)}" : $"{BaseNamesGenerator.GetInterfaceName(group)}");

        private static string GenerateClassDeclaration(
            IGrouping<string, TypeDeclarationSyntax> group,
            bool isDesignPatternPostfix = false,
            bool isMainAttributeOnInterface = false,
            bool isPartialClass = false,
            GeneratorAttributeType attributeType = GeneratorAttributeType.Pattern)
        {
            var baseClassDeclaration =
                $"{BaseNamesGenerator.GetAccesibility(group)}{GeneratePartialKeyword(isPartialClass)} " +
                $"class {BaseNamesGenerator.GetClassName(group, attributeType, isDesignPatternPostfix, isMainAttributeOnInterface)}";

            return isMainAttributeOnInterface
            ? $"{baseClassDeclaration}: {BaseNamesGenerator.GetInterfaceName(group)}"
            : baseClassDeclaration;

        }
        private static string GeneratePartialKeyword(bool isPartialClass)
            => isPartialClass ? " partial" : "";

        private static string GenerateEnumDeclaration(
            IGrouping<string, TypeDeclarationSyntax> group,
            bool isDesignPatternPostfix = false,
            bool isMainAttributeOnInterface = false,
            GeneratorAttributeType attributeType = GeneratorAttributeType.Pattern)
            => $"{BaseNamesGenerator.GetAccesibility(group)} enum {BaseNamesGenerator.GetClassName(group, attributeType, isDesignPatternPostfix, isMainAttributeOnInterface)}Type";
    }
}
