using System;

namespace DesignPatternCodeGenerator.Attributes.NullObject
{
    /// <summary>
    /// Attribute to generate the Null Object design pattern.
    /// This attribute must be applied to the interface so that the generator creates a Null Object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class NullObjectAttribute : Attribute { }
}
