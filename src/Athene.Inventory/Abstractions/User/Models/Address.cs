namespace Athene.Inventory.Abstractions.Models
{
    public class Address
    {
        private Address()
        {
        }

        public Address(string street, string zip, string city, string country)
        {
            Street = street;
            Zip = zip;
            City = city;
            Country = country;
        }

        public int Id { get; private set; }
        public string Street { get; set; }
        public string Zip { get;  set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
