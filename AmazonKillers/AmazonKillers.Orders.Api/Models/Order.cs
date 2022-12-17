namespace AmazonKillers.Orders.Api.Models
{
    public enum Status
    {
        Cancelled,
        AwaitingDelivery,
        InDelivery,
        Delivered,
        Completed
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        Online
    }

    public enum DeliveryType
    {
        Delivery,
        Pickup
    }

    public class Order : AddressBase
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public bool IsPayed { get; set; }
        public Status Status { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public Order() { }

    }
}
