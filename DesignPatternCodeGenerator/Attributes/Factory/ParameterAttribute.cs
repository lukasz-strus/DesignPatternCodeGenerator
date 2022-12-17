using System;

namespace DesignPatternCodeGenerator.Attributes.Factory
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class ParameterAttribute : Attribute { }
}
