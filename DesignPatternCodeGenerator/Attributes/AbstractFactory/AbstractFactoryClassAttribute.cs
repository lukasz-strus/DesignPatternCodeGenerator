using System;

namespace DesignPatternCodeGenerator.Attributes.AbstractFactory
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AbstractFactoryClassAttribute : Attribute
    {
        public string ChildFactoryName { get; }

        public AbstractFactoryClassAttribute(string childFactoryName)
        {
            ChildFactoryName = childFactoryName;
        }
    }
}
