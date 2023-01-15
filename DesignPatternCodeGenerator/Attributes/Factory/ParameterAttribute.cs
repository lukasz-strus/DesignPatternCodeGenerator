using System;

namespace DesignPatternCodeGenerator.Attributes.Factory
{
    /// <summary>
    /// Attribute to generate the Factory design pattern.
    /// This attribute should be applied to the property that is to be provided when creating individual objects.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class ParameterAttribute : Attribute { }
}
