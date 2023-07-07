namespace DsDelivery.Core.Domain
{
    public class Address
    {
        public int CustomerId { get; set; }
        public int Cep { get; set; }
        public State State { get; set; }
        public string City { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public Customer Customer { get; set; }
    }
}
