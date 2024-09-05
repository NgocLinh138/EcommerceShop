namespace Ordering.Infrastructure.Data.Extensions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("E830D7C5-E763-4058-BEC9-A67DA8D01CD2")), "An", "an@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("A8ED8766-536B-427D-82C0-7416D66BB58E")), "Binh", "binh@gmail.com")
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("249DAF5F-B742-4F49-88D5-8F55D8A484C6")), "Basic T-Shirt", 150),
                Product.Create(ProductId.Of(new Guid("B64E7A34-AF8D-4A56-917C-6F49D8D0BB51")), "Denim Jeans", 550),
                Product.Create(ProductId.Of(new Guid("5D3545D4-939A-4BC1-9C2D-098C1C6A84A5")), "Hooded Sweatshirt", 350),
            };

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("An", "Nguyen", "an@gmail.com", "District 9", "Viet Nam", "Ho Chi Minh City", "71200");
                var address2 = Address.Of("Binh", "Nguyen", "binh@gmail.com", "District 8", "Viet Nam", "Ho Chi Minh City", "73017");

                var payment1 = Payment.Of("Nguyen An", "4456530000001005", "12/24", "123", 1);
                var payment2 = Payment.Of("Nguyen Binh", "4456530000001096", "12/24", "123", 2);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("E830D7C5-E763-4058-BEC9-A67DA8D01CD2")),
                    OrderName.Of("ORD_1"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment1);

                order1.Add(ProductId.Of(new Guid("249DAF5F-B742-4F49-88D5-8F55D8A484C6")), 2, 150);
                order1.Add(ProductId.Of(new Guid("B64E7A34-AF8D-4A56-917C-6F49D8D0BB51")), 1, 550);


                var order2 = Order.Create(
                   OrderId.Of(Guid.NewGuid()),
                   CustomerId.Of(new Guid("A8ED8766-536B-427D-82C0-7416D66BB58E")),
                   OrderName.Of("ORD_2"),
                   shippingAddress: address2,
                   billingAddress: address2,
                   payment2);

                order2.Add(ProductId.Of(new Guid("5D3545D4-939A-4BC1-9C2D-098C1C6A84A5")), 1, 350);

                return new List<Order> { order1, order2 };  
            }
        }
    }
}
