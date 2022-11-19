using System;

namespace DesignPatternCodeGenerator.Attributes.Factory
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FactoryProductAttribute : Attribute
    {
        public FactoryProductAttribute(){ }
    }
}
