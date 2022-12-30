using System;

namespace DesignPatternCodeGenerator.Attributes.IoCContainer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ContainerAttribute : Attribute
    {
        public ContainerAttribute(
            string containerName,
            ObjectLifeTime objectLifeTime,            
            string[] excludedInterfaces)
        {
            ObjectLifeTime = objectLifeTime;
            ContainerName = containerName;
            ExcludedInterfaces = excludedInterfaces;
        }

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
