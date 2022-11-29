using System;

namespace DesignPatternCodeGenerator.Attributes.Facade
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FacadeMethodAttribute : Attribute 
    {
        public string FacadeName { get; set; }

        public FacadeMethodAttribute(string facadeName)
        {
            FacadeName = facadeName;
        }
    }
}
