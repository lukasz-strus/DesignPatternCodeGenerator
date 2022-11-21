using System;

namespace DesignPatternCodeGenerator.Attributes.AbstractFactory
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AbstractFactoryClassAttribute : Attribute
    {
        public string FactoryClassName { get; }

        public AbstractFactoryClassAttribute(string factoryClassName)
        {
            FactoryClassName = factoryClassName;
        }
    }
}
