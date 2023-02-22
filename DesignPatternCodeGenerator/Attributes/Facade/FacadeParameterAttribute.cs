using System;

namespace DesignPatternCodeGenerator.Attributes.Facade
{
    /// <summary>
    /// Attribute to generate the Facade design pattern.
    /// This parameter should be applied to the parameter, and in the constructor it takes the method path.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class FacadeParameterAttribute : Attribute 
    {
        public string MethodName { get; set; }

        /// <summary>
        /// Attribute to generate the Facade design pattern
        /// This parameter should be applied to the parameter, and in the constructor it takes the method path.
        /// </summary>
        /// <param name="methodName">
        /// The name of the method whose result should be assigned to this parameter.
        /// The name should consist of the class name and method name, separated by a dot. 
        /// For example: "ClassName.MethodName"
        /// </param>
        public FacadeParameterAttribute(string methodName)
        {
            MethodName = methodName;
        }

    }
}
