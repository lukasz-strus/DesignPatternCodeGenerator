using System;

namespace DesignPatternCodeGenerator.Factory
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class FactoryAttribute : Attribute { }
}
