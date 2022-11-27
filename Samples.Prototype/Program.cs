using Samples.Prototype;

var person = new Person()
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