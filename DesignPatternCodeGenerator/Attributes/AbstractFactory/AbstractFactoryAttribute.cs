using System;

namespace DesignPatternCodeGenerator.Attributes.AbstractFactory
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class AbstractFactoryAttribute : Attribute
    {
        public string MainFactoryName { get; }

        public AbstractFactoryAttribute(string mainFactoryName)
        {
            MainFactoryName = mainFactoryName;
        }
    }
}
