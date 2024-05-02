namespace SDWrox.Entity
{
    public class CustomOrder
    {
        public int Id { get; set; }

        public int ShopId { get; set; }

        public string ShopName { get; set; }

        public string Number { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Owner { get; set; } = null!;

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Count { get; set; }

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }


    }
}
