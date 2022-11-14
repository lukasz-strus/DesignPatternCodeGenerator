using System;

namespace DesignPatternCodeGenerator.Attributes.Singleton
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SingletonAttribute : Attribute { }
}
