# DesignPatternCodeGenerator
The library is used to generate the code of typical design patterns in C #, based on the capabilities of the .NET Compiler Platform SDK (Roslyn APIs).
More information about Roslyn APIs: https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/ 

## How to start using the library?
Edit your project and add the following, replacing the project path with the one from the .NET Standard project you created above:
```xml
  <ItemGroup>
	<ProjectReference Include="..\PathTo\DesignPatternCodeGenerator.csproj"
					  OutputItemType="Analyzer"
					  ReferenceOutputAssembly="true" />
    <ProjectReference Include="..\PathTo\DesignPatternCodeGenerator.csproj" />
  </ItemGroup>
```

The generated files can be seen in the project tree: 

Dependencies -> Analyzers -> 

![image](https://user-images.githubusercontent.com/61932823/202016757-a77d9caa-4e74-4714-8609-faaf1ae899f6.png)
##

## Prototype
The prototype pattern is genereted based on one attribute:
- [Prototype] - this attribute should be applied to the class that is about to become a prototype. The class must be a partial class.

### Example:
```csharp
using DesignPatternCodeGenerator.Attributes.Prototype;

namespace Samples.Prototype;

[Prototype]
public partial class Person
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public Address? PersonAddress { get; set; }
    public Contacts? PersonContacts { get; set; }
}

public class Address
{
    public string? HouseNumber { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
}

public class Contacts
{
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
```
Using the code above, the generator will generate a partial class with an implemented prototype with the ShallowCopy() and DeepCopy() methods.

```csharp
// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.Prototype;

namespace Samples.Prototype
{
    public partial class Person
    {
        public Person ShallowCopy()
        {
            return (Person)this.MemberwiseClone();
        }

        public Person DeepCopy()
        {
            Person clone = (Person)this.MemberwiseClone();

            clone.PersonAddress = new Address()
            {
	    	HouseNumber = PersonAddress.HouseNumber,
		Street = PersonAddress.Street,
		City = PersonAddress.City
            };

	    clone.PersonContacts = new Contacts()
            {
	    	PhoneNumber = PersonContacts.PhoneNumber,
		Email = PersonContacts.Email
            };

            return clone;
        }
    }
}
```

## AbstractFactory
The abstract factory pattern is generated based on two attributes:
- [AbstractFactory("MainInterfaceName")] - this attribute should be appiled to interface,
- [AbstractFactoryClass("FactoryClassName")] - this attribute should be appiled to class.

### Example:
```csharp
using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory;

[AbstractFactory("UIElement")]
public interface IButton
{
    void Render();
    void HandleClick();
}

[AbstractFactoryClass("Windows")]
public class WindowsButton : IButton
{
    public void HandleClick()
    {
        Console.WriteLine("Handle windows click event");
    }

    public void Render()
    {
        Console.WriteLine("Render windows button");
    }
}

[AbstractFactoryClass("Mac")]
public class MacButton : IButton
{
    public void HandleClick()
    {
        Console.WriteLine("Handle mac click event");
    }

    public void Render()
    {
        Console.WriteLine("Render mac button");
    }
}
```

```csharp
using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory;

[AbstractFactory("UIElement")]
public interface ITextBox
{
    void Render();
    void HandleInput();
}

[AbstractFactoryClass("Windows")]
public class WindowsTextBox : ITextBox
{
    public void HandleInput()
    {
        Console.WriteLine("Handle windows text input");
    }

    public void Render()
    {
        Console.WriteLine("Render windows textbox");
    }
}

[AbstractFactoryClass("Mac")]
public class MacTextBox : ITextBox
{
    public void HandleInput()
    {
        Console.WriteLine("Handle mac text input");
    }

    public void Render()
    {
        Console.WriteLine("Render mac textbox");
    }
}
```
Using the code above, the generator will generate an abstract factory which consists of a main factory interface and factory classes.

```csharp
// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory
{
    public interface IUIElementFactory
    {
    	ITextBox CreateTextBox();
	IButton CreateButton();
    }
}
```
```csharp
// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory
{
    public class WindowsFactory : IUIElementFactory
    {
        
        public ITextBox CreateTextBox()
        {
            return new WindowsTextBox();
        }

        public IButton CreateButton()
        {
            return new WindowsButton();
        }
    }
}
```
```csharp
// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory
{
    public class MacFactory : IUIElementFactory
    {
        
        public ITextBox CreateTextBox()
        {
            return new MacTextBox();
        }

        public IButton CreateButton()
        {
            return new MacButton();
        }
    }
}
```


## Factory
The factory pattern is generated based on three attributes:
- [Factory] - this attribute should be applied to the main interface,
- [Parameter] - this attribute should be applied to the property that is to be provided when creating individual objects,
- [FactoryChild] - this attribute should be applied to the class that implements the main interface.

### Example:
```csharp
using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Factory;

namespace Samples.Factory;
[Factory]
public interface ICar
{
    [Parameter]
    public string Name { get; set; }        
    public int HorsePower { get; set; }

}

[FactoryChild]
class Bmw : ICar
{
    public string Name { get; set; }
    public int HorsePower { get; set; }

    public Bmw(string name, int horsePower)
    {
        Name = name;
        HorsePower = horsePower;
    }
}

[FactoryChild]
partial class Audi : ICar
{
    public string Name { get; set; }
    public int HorsePower { get; set; }

    public Audi(string name, int horsePower)
    {
        Name = name;
        HorsePower = horsePower;
    }
}
```

Using the code above, the generator will generate a factory which consists of a factory interface, a factory class, and an enum type.

```csharp
// <auto-generated/>
using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Factory;

namespace Samples.Factory
{
    public interface ICarFactory
    {
        public ICar Create(CarFactoryType type,string Name);
    }
}
```

```csharp
// <auto-generated/>
using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Factory;

namespace Samples.Factory
{
    public class CarFactory: ICarFactory
    {
        private readonly int _horsepower;
        
        public CarFactory(int HorsePower)
        {
            _horsepower = HorsePower;
        }
        
        public ICar Create(CarFactoryType type,string Name)
        {
          switch (type)
          {
              case CarFactoryType.Bmw :
                  return new Bmw(Name, _horsepower);
               case CarFactoryType.Audi :
                  return new Audi(Name, _horsepower);
               default :
                  throw new Exception($"Type {type} is not handled");
            }    
        }
    }
}
```

```csharp
// <auto-generated/>
using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Factory;

namespace Samples.Factory
{
    public enum CarFactoryType
    {
        Bmw,
        Audi,
    }
}
```
##
## Singleton
The singleton pattern is generated based on only one attribute:
- [Singleton] - this attribute should be applied to the class that is about to become a singleton. The class must be a partial class.

### Example
```csharp
[Singleton]
public partial class Configuration
{
    public string SomeProperty { get; set; }
}
```
Using the code above, the generator will generate a partial class with the singleton implemented.

```csharp
// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.Singleton;

namespace Samples.Singleton
{
    public partial class Configuration
    {
    	private static Configuration _instance = null;
	private static object obj = new object();
	
	private Configuration() { }
	
	public static Configuration GetInstance()
	{
            lock(obj)
            {
                if (_instance == null)
                {
                    _instance = new Configuration();
                }
            }

        return _instance;
        }
    }
}
```

