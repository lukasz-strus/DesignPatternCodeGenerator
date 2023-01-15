using System;

namespace DesignPatternCodeGenerator.Attributes.Factory
{
    /// <summary>
    /// Attribute to generate the Factory design pattern.
    /// This attribute must be applied to the main interface so that the generator creates a Factory.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class FactoryAttribute : Attribute { }
}
