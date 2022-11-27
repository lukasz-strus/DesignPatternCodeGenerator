﻿using DesignPatternCodeGenerator.Attributes.Prototype;

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
