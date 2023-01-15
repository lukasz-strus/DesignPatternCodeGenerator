using System;

namespace DesignPatternCodeGenerator.Attributes.AbstractFactory
{
    /// <summary>
    /// Attribute to generate the Abstract Factory design pattern.
    /// This attribute should be applied to interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class AbstractFactoryAttribute : Attribute
    {
        public string MainFactoryName { get; }

        /// <summary>
        /// Attribute to generate the Abstract Factory design pattern.
        /// This attribute should be applied to interface.
        /// </summary>
        /// <param name="mainFactoryName">
        /// The name of the abstract factory to be generated with.
        /// </param>
        public AbstractFactoryAttribute(string mainFactoryName)
        {
            MainFactoryName = mainFactoryName;
        }
    }
}
