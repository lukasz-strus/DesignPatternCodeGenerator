using Samples.Prototype;

var personAddress = new Address()
{
    Street = "Złota",
    HouseNumber = "44",
    City = "Warsaw"
};

var personContacts = new Contacts()
{
    Email = "john.smith@gmail.com",
    PhoneNumber = "987-654-321"
};


var person = new Person("John", "Smith", personAddress, personContacts);

var person10 = new Person2()
{
    Name = "John",
    LastName = "Smith",
    PersonAddress = new()
    {
        Street = "Złota",
        HouseNumber = "44",
        City = "Warsaw"
    },
    PersonContacts = new()
    {
        Email = "john.smith@gmail.com",
        PhoneNumber = "987-654-321"
    }
};

var person1 = person.ShallowCopy();

bool referenceEquals = ReferenceEquals(person, person1);
Console.WriteLine($"Reference Equals (ShallowCopy): {referenceEquals}");

bool personAddressReferenceEuqals = ReferenceEquals(person.PersonAddress, person1.PersonAddress);
Console.WriteLine($"PersonAddress Reference Equals (ShallowCopy): {personAddressReferenceEuqals}");

bool personContactsReferenceEuqals = ReferenceEquals(person.PersonContacts, person1.PersonContacts);
Console.WriteLine($"PersonContacts Reference Equals (ShallowCopy): {personContactsReferenceEuqals}");
Console.WriteLine();

var person2 = person.DeepCopy();

referenceEquals = ReferenceEquals(person, person2);
Console.WriteLine($"Reference Equals (DeepCopy): {referenceEquals}");

personAddressReferenceEuqals = ReferenceEquals(person.PersonAddress, person2.PersonAddress);
Console.WriteLine($"PersonAddress Reference Equals (DeepCopy): {personAddressReferenceEuqals}");

personContactsReferenceEuqals = ReferenceEquals(person.PersonContacts, person2.PersonContacts);
Console.WriteLine($"PersonContacts Reference Equals (DeepCopy): {personContactsReferenceEuqals}");


var person21 = person10.ShallowCopy();

referenceEquals = ReferenceEquals(person10, person21);
Console.WriteLine($"Reference Equals (ShallowCopy): {referenceEquals}");

personAddressReferenceEuqals = ReferenceEquals(person10.PersonAddress, person21.PersonAddress);
Console.WriteLine($"PersonAddress Reference Equals (ShallowCopy): {personAddressReferenceEuqals}");

 personContactsReferenceEuqals = ReferenceEquals(person10.PersonContacts, person21.PersonContacts);
Console.WriteLine($"PersonContacts Reference Equals (ShallowCopy): {personContactsReferenceEuqals}");
Console.WriteLine();

var person22 = person10.DeepCopy();

referenceEquals = ReferenceEquals(person10, person22);
Console.WriteLine($"Reference Equals (DeepCopy): {referenceEquals}");

personAddressReferenceEuqals = ReferenceEquals(person10.PersonAddress, person22.PersonAddress);
Console.WriteLine($"PersonAddress Reference Equals (DeepCopy): {personAddressReferenceEuqals}");

personContactsReferenceEuqals = ReferenceEquals(person10.PersonContacts, person22.PersonContacts);
Console.WriteLine($"PersonContacts Reference Equals (DeepCopy): {personContactsReferenceEuqals}");