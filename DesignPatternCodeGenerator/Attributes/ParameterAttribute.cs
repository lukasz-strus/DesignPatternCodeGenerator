using System;

namespace DesignPatternCodeGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class ParameterAttribute : Attribute { }
}
