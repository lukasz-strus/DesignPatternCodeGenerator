using System;

namespace DesignPatternCodeGenerator.Factory
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class FactoryAttribute : Attribute { }
}
