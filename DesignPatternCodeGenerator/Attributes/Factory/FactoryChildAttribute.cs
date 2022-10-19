using System;

namespace DesignPatternCodeGenerator.Attributes.Factory
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FactoryChildAttribute : Attribute
    {
        public Type Factory { get; }

        /// <param name="factory">The attribute parameter must be an interface</param>
        public FactoryChildAttribute(Type factory)
        {
            Factory = factory.IsInterface ? factory :
                throw new ArgumentException("The attribute parameter must be an interface");
        }
    }
}
