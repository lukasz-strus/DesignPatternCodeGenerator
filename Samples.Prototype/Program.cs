using Samples.Prototype;

var personAddress = new Address()
{
    Street = "Zlota",
    HouseNumber = "44",
    City = "Warsaw"
};

var personContacts = new Contacts()
{
    Email = "john.smith@gmail.com",
    PhoneNumber = "987-654-321"
};

var person = new Person("John", "Smith", personAddress, personContacts);

var shallowPerson = person.ShallowCopy();

bool referenceEquals = ReferenceEquals(person, shallowPerson);
Console.WriteLine($"Reference Equals (ShallowCopy): {referenceEquals}");

bool personAddressReferenceEuqals = ReferenceEquals(person.PersonAddress, shallowPerson.PersonAddress);
Console.WriteLine($"PersonAddress Reference Equals (ShallowCopy): {personAddressReferenceEuqals}");

bool personContactsReferenceEuqals = ReferenceEquals(person.PersonContacts, shallowPerson.PersonContacts);
Console.WriteLine($"PersonContacts Reference Equals (ShallowCopy): {personContactsReferenceEuqals}");
Console.WriteLine();

var deepPerson = person.DeepCopy();

referenceEquals = ReferenceEquals(person, deepPerson);
Console.WriteLine($"Reference Equals (DeepCopy): {referenceEquals}");

personAddressReferenceEuqals = ReferenceEquals(person.PersonAddress, deepPerson.PersonAddress);
Console.WriteLine($"PersonAddress Reference Equals (DeepCopy): {personAddressReferenceEuqals}");

personContactsReferenceEuqals = ReferenceEquals(person.PersonContacts, deepPerson.PersonContacts);
Console.WriteLine($"PersonContacts Reference Equals (DeepCopy): {personContactsReferenceEuqals}");