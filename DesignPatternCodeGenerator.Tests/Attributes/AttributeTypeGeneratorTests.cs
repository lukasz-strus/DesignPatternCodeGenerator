using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Factory;
using DesignPatternCodeGenerator.Base.Enums;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Attributes;

public class AttributeTypeGeneratorTests
{
    [Theory]
    [InlineData(GeneratorAttributeType.Factory, typeof(FactoryAttribute))]
    [InlineData(GeneratorAttributeType.FactoryChild, typeof(FactoryChildAttribute))]
    internal void SetGeneratorAttributeType_ForValidInputs_ReturnsType(GeneratorAttributeType generatorType, Type attributeType)
    {
        //act

        var result = AttributeTypeGenerator.SetGeneratorAttributeType(generatorType);

        //assert

        result.Should().Be(attributeType);
    }

    [Theory]
    [InlineData(GeneratorAttributeType.Builder)]
    internal void SetGeneratorAttributeType_ForInvalidInputs_ReturnsType(GeneratorAttributeType generatorType)
    {
        //act

        Action act = () => AttributeTypeGenerator.SetGeneratorAttributeType(generatorType);

        //assert

        act.Should().Throw<Exception>().WithMessage($"Type {generatorType} is not handled");
    }
}
