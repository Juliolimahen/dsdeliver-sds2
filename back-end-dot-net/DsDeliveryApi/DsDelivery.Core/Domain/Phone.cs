namespace DsDelivery.Core.Domain
{
    public class Phone
    {
        public int CustomerId { get; set; }
        public string Number { get; set; }
        public Customer Customer { get; set; }
    }
}