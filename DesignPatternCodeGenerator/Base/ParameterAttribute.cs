using System;

namespace DesignPatternCodeGenerator.Base
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class ParameterAttribute : Attribute { }
}
