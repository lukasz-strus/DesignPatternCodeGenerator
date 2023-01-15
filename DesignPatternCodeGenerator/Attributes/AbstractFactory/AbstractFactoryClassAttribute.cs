using System;

namespace DesignPatternCodeGenerator.Attributes.AbstractFactory
{
    /// <summary>
    /// Attribute to generate the Abstract Factory design pattern.
    /// This attribute should be applied to a class that implements an interface marked with the AbstractFactoryAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AbstractFactoryClassAttribute : Attribute
    {
        public string FactoryClassName { get; }

        /// <summary>
        /// Attribute to generate the Abstract Factory design pattern.
        /// This attribute should be applied to a class that implements an interface marked with the AbstractFactoryAttribute.
        /// </summary>
        /// <param name="factoryClassName">
        /// The name of the factory to be generated with.
        /// </param>
        public AbstractFactoryClassAttribute(string factoryClassName)
        {
            FactoryClassName = factoryClassName;
        }
    }
}
