using DesignPatternCodeGenerator.Attributes.Factory;
using DesignPatternCodeGenerator.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                default:
                    throw new Exception($"Type {generatorType} is not handled");
            }
        }
    }
}
