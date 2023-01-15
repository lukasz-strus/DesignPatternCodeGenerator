using System;

namespace DesignPatternCodeGenerator.Attributes.Facade
{
    /// <summary>
    /// Attribute to generate the Facade design pattern.
    /// This attribute should be applied to the facade method parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class FacadeMainParameterAttribute : Attribute
    {
        public string MainParameterName { get; set; }

        /// <summary>
        /// Attribute to generate the Facade design pattern.
        /// This attribute should be applied to the facade method parameter.
        /// </summary>
        /// <param name="mainParameterName">
        /// The name of the parameter that the Facade method will contain.
        /// </param>
        public FacadeMainParameterAttribute(string mainParameterName)
        {
            MainParameterName = mainParameterName;
        }
    }
}
