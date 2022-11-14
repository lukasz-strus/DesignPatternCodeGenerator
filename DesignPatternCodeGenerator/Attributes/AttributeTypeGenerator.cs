using DesignPatternCodeGenerator.Attributes.Factory;
using DesignPatternCodeGenerator.Attributes.Singleton;
using DesignPatternCodeGenerator.Base.Enums;
using System;

namespace DesignPatternCodeGenerator.Attributes
{
    internal static class AttributeTypeGenerator
    {
        internal static Type SetGeneratorAttributeType(GeneratorAttributeType generatorType)
        {
            switch (generatorType)
            {
                case GeneratorAttributeType.Factory:
                    return typeof(FactoryAttribute);
                case GeneratorAttributeType.FactoryChild:
                    return typeof(FactoryChildAttribute);
                case GeneratorAttributeType.Singleton:
                    return typeof(SingletonAttribute);
                default:
                    throw new Exception($"Type {generatorType} is not handled");
            }
        }
    }
}
