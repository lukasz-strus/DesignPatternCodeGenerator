using System;

namespace DesignPatternCodeGenerator.Attributes.Prototype
{
    /// <summary>
    /// Attribute to generate the Prototype design pattern.
    /// This attribute should be applied to the class that is about to become a prototype.
    /// The class must be a partial class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PrototypeAttribute : Attribute { }
}
