using System;

namespace DesignPatternCodeGenerator.Attributes.Singleton
{
    /// <summary>
    /// Attribute to generate the Singleton design pattern.
    /// This attribute should be applied to the class that is about to become a singleton.
    /// The class must be a partial class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SingletonAttribute : Attribute { }
}
