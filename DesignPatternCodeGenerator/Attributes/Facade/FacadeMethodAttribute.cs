using System;

namespace DesignPatternCodeGenerator.Attributes.Facade
{
    /// <summary>
    /// Attribute to generate the Facade design pattern.
    /// This attribute should be applied to the method that is about to become a facade method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FacadeMethodAttribute : Attribute 
    {
        public string FacadeName { get; set; }

        /// <summary>
        /// Attribute to generate the Facade design pattern.
        /// This attribute should be applied to the method that is about to become a facade method.
        /// </summary>
        /// <param name="facadeName">
        /// The name of the Facade in which this method is to be called.
        /// </param>
        public FacadeMethodAttribute(string facadeName)
        {
            FacadeName = facadeName;
        }
    }
}
