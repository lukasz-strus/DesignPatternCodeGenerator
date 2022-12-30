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
    [InlineData(GeneratorAttributeType.FactoryProduct, typeof(FactoryProductAttribute))]
    internal void SetGeneratorAttributeType_ForValidInputs_ReturnsType(GeneratorAttributeType generatorType, Type attributeType)
    {
        var result = AttributeTypeGenerator.CreateGeneratorAttributeType(generatorType);

        result.Should().Be(attributeType);
    }

    [Theory]
    [InlineData(GeneratorAttributeType.Builder)]
    internal void SetGeneratorAttributeType_ForInvalidInputs_ReturnsType(GeneratorAttributeType generatorType)
    {
        Action act = () => AttributeTypeGenerator.CreateGeneratorAttributeType(generatorType);

        act.Should().Throw<Exception>().WithMessage($"Type {generatorType} is not handled");
    }
}
