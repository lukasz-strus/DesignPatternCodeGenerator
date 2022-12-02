using DesignPatternCodeGenerator.Attributes.Prototype;

namespace Samples.Prototype;

[Prototype]
public partial class Person
{
    public Person(string? name, string? lastName, Address? personAddress, Contacts? personContacts)
    {
        Name = name;
        LastName = lastName;
        PersonAddress = personAddress;
        PersonContacts = personContacts;
    }

    public string? Name { get; set; }
    public string? LastName { get; set; }
    public Address? PersonAddress { get; set; }
    public Contacts? PersonContacts { get; set; }
}

[Prototype]
public partial class Person2
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
