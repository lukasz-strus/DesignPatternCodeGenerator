using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using DesignPatternCodeGenerator.Attributes.IoCContainer;
using DesignPatternCodeGenerator.Attributes.Facade;
using DesignPatternCodeGenerator.Attributes.Factory;
using DesignPatternCodeGenerator.Attributes.NullObject;
using DesignPatternCodeGenerator.Attributes.Prototype;
using DesignPatternCodeGenerator.Attributes.Singleton;
using DesignPatternCodeGenerator.Base.Enums;
using System;

namespace DesignPatternCodeGenerator.Attributes
{
    internal static class AttributeTypeGenerator
    {
        internal static Type CreateGeneratorAttributeType(GeneratorAttributeType generatorType)
        {
            switch (generatorType)
            {
                case GeneratorAttributeType.Factory:
                    return typeof(FactoryAttribute);
                case GeneratorAttributeType.FactoryProduct:
                    return typeof(FactoryProductAttribute);
                case GeneratorAttributeType.Singleton:
                    return typeof(SingletonAttribute);
                case GeneratorAttributeType.AbstractFactory:
                    return typeof(AbstractFactoryAttribute);
                case GeneratorAttributeType.AbstractFactoryClass:
                    return typeof(AbstractFactoryClassAttribute);
                case GeneratorAttributeType.Prototype:
                    return typeof(PrototypeAttribute);
                case GeneratorAttributeType.FacadeMethod:
                    return typeof(FacadeMethodAttribute);
                case GeneratorAttributeType.FacadeParameter:
                    return typeof(FacadeParameterAttribute);
                case GeneratorAttributeType.FacadeMainParameter:
                    return typeof(FacadeMainParameterAttribute);
                case GeneratorAttributeType.NullObject:
                    return typeof(NullObjectAttribute);
                case GeneratorAttributeType.Container:
                    return typeof(ContainerAttribute);
                default:
                    throw new Exception($"Type {generatorType} is not handled");
            }
        }
    }
}
