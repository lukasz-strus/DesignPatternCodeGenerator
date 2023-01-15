using System;

namespace DesignPatternCodeGenerator.Attributes.Factory
{
    /// <summary>
    /// Attribute to generate the Factory design pattern.
    /// This attribute should be applied to the class that implements the main interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FactoryProductAttribute : Attribute { }
}
