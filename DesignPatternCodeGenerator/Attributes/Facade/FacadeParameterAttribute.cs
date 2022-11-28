using System;

namespace DesignPatternCodeGenerator.Attributes.Facade
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class FacadeParameterAttribute : Attribute 
    {
        public string Parent { get; set; }

        public FacadeParameterAttribute(string parent)
        {
            Parent = parent;
        }

    }
}
