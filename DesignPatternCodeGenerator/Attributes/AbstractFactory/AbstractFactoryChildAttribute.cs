using System;

namespace DesignPatternCodeGenerator.Attributes.AbstractFactory
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AbstractFactoryChildAttribute : Attribute
    {
        public string ChildFactoryName { get; }

        public AbstractFactoryChildAttribute(string childFactoryName)
        {
            ChildFactoryName = childFactoryName;
        }
    }
}
