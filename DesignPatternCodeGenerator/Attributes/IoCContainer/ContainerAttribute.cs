using System;

namespace DesignPatternCodeGenerator.Attributes.IoCContainer
{
    /// <summary>
    /// The attribute to register the class to the IServiceCollection container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ContainerAttribute : Attribute
    {
        /// <summary>
        /// The attribute to register the class to the IServiceCollection container.
        /// </summary>
        /// <param name="containerName">
        /// The name of the container in which this class will be registered as a service for the implemented interfaces.
        /// </param>
        /// <param name="objectLifeTime">
        /// Lifespan of the service in the IoC container.
        /// </param>
        /// <param name="excludedInterfaces">
        /// Interfaces for which this class will not be registered as a service.
        /// </param>
        public ContainerAttribute(
            string containerName,
            ObjectLifeTime objectLifeTime,            
            string[] excludedInterfaces)
        {
            ObjectLifeTime = objectLifeTime;
            ContainerName = containerName;
            ExcludedInterfaces = excludedInterfaces;
        }

        /// <summary>
        /// The attribute to register the class to the IServiceCollection container.
        /// </summary>
        /// <param name="containerName">
        /// The name of the container in which this class will be registered as a service for the implemented interfaces.
        /// </param>
        /// <param name="objectLifeTime">
        /// Interfaces for which this class will not be registered as a service.
        /// </param>
        public ContainerAttribute(
            string containerName,
            ObjectLifeTime objectLifeTime)
        {
            ObjectLifeTime = objectLifeTime;
            ContainerName = containerName;
            ExcludedInterfaces = new string[] { };
        }

        public ObjectLifeTime ObjectLifeTime { get; set; }
        public string ContainerName { get; set; }
        public string[] ExcludedInterfaces { get; set; }
    }

}
