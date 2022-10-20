using System;

namespace DesignPatternCodeGenerator.Attributes.Factory
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FactoryChildAttribute : Attribute
    {
        public FactoryChildAttribute(){ }
    }
}
