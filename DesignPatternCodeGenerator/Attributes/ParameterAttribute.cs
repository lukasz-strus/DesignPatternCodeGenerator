using System;

namespace DesignPatternCodeGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class ParameterAttribute : Attribute { }
}
